using Game.BehaviourTree;
using Game.GameBase;
using Game.Team;
using Game.Town;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Family
{
    /// <summary>
    /// 家族控制器，处理家族的逻辑
    /// </summary>
    public class BaseFamily : BaseGameController, IGetBehaviourTree
    {
        public BehaviourTree.BehaviourTree behaviourTree;
        private int _familyId;
        private FamilyData _familyData;
        private FamilyBlackBoard _familyBlackBoard;
        private FamilyAiAgent _familyAiAgent;
        private GameObject _teamPrefab;

        /// <summary>
        /// 是否每月尝试组建一支队伍（默认是每天尝试一次）
        /// </summary>
        private bool _checkBuildTeamByMonth;

        /// <summary>
        /// 尝试组建军队的时间
        /// </summary>
        private GameTime _checkBuildTeamTime;

        protected override void OnInit()
        {
            base.OnInit();
            _teamPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.TEAM);
            behaviourTree = behaviourTree.Clone();
            _familyBlackBoard = new FamilyBlackBoard
            {
                buildTeam = BuildTeam
            };
            behaviourTree.blackboard = _familyBlackBoard;
            _familyAiAgent = this.AddComponent<FamilyAiAgent>();
            behaviourTree.Bind(_familyAiAgent);
        }

        protected override void OnControllerStart()
        {
        }

        /// <summary>
        /// 初始化所有数据
        /// </summary>
        /// <param name="familyId"></param>
        public void Init(int familyId)
        {
            _familyId = familyId;
            _familyData = this.GetModel<IFamilyModel>().FamilyData[_familyId];
            _familyBlackBoard.familyId = _familyId;
            _familyBlackBoard.familyData = _familyData;
            name = this.GetSystem<IGameSystem>().GetDataName(_familyData.storage.name);
            _familyAiAgent.Init(_familyBlackBoard);
            _checkBuildTeamTime = GameTime.GetRandomTime(false, false, _checkBuildTeamByMonth);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(_ =>
            {
                if (this.GetModel<IGameModel>().NowTime.Equals(_checkBuildTeamTime))
                {
                    behaviourTree.StartTree();
                }

                if (this.GetModel<IGameModel>().NowTime.Equals(GameTime.RefreshEconomyTime))
                {
                    UpdateEconomy();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            behaviourTree.UpdateTree();
        }

        /// <summary>
        /// 刷新家族经济
        /// </summary>
        private void UpdateEconomy()
        {
            //税收
            int taxation = 0;
            for (int i = 0; i < _familyData.storage.familyTownS.Count; i++)
            {
                TownData townData = this.GetModel<ITownModel>().TownData[_familyData.storage.familyTownS[i]];
                taxation += (int)(townData.noStorage.prosperity * FamilyConstant.PROSPERITY_TAX_COEFFICIENT);
            }

            //工场收入
            int workShopRevenue = 1000; //暂定工场收入
            _familyData.UpdateWealth(taxation + workShopRevenue);
            Debug.Log("家族财富刷新：" + this.GetSystem<IGameSystem>().GetDataName(_familyData.storage.name) + "  " +
                      _familyData.storage.familyWealth);
        }

        /// <summary>
        /// 组建一支队伍
        /// </summary>
        /// <param name="roleId"></param>
        private void BuildTeam(int roleId)
        {
            this.GetModel<IFamilyModel>().RoleData[roleId].roleType = RoleType.GENERAL;
            GameObject teamObject = Instantiate(_teamPrefab);
            teamObject.Parent(this.GetModel<IGameModel>().PlayerTeam.transform.parent);
            int townId = this.GetModel<IFamilyModel>().RoleData[roleId].townId;
            TownCommonData townCommonData = this.GetModel<ITownModel>().TownCommonData[townId];
            teamObject.Position(new Vector3(townCommonData.TownPos[0], townCommonData.TownPos[1]));
            Team.Team team = teamObject.GetComponent<Team.Team>();
            int teamId = this.GetSystem<ITeamSystem>().AddTeam(new TeamData
            {
                generalRoleId = roleId,
                number = 1,
                teamType = TeamType.HUT_TOWN,
                townId = townId
            });
            team.InitTeam(teamId, _familyId);
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}