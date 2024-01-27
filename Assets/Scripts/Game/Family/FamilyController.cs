using Game.BehaviourTree;
using GameQFramework;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Constant;
using Random = System.Random;

namespace Game.Family
{
    /// <summary>
    /// 家族控制器，处理家族的逻辑
    /// </summary>
    public class FamilyController : BaseGameController, IGetBehaviourTree
    {
        public BehaviourTree.BehaviourTree behaviourTree;
        private int _familyId;
        private FamilyBlackBoard _familyBlackBoard;
        private FamilyAiAgent _familyAiAgent;
        private GameObject _teamPrefab;

        /// <summary>
        /// 是否每月尝试组建一支队伍（默认是每天尝试一次）
        /// </summary>
        private bool _checkBuildTeamByMonth;

        /// <summary>
        /// 每月的第几天尝试组建队伍
        /// </summary>
        private int _checkBuildTeamDay;

        /// <summary>
        /// 每天的第几个时辰尝试组建队伍
        /// </summary>
        private int _checkBuildTeamTime;

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
            name = this.GetModel<IFamilyModel>().FamilyData[_familyId].familyName;
            _familyAiAgent.Init(_familyId, _familyBlackBoard);

            Random random = new Random();
            _checkBuildTeamDay = random.Next(GameTimeConstant.DAY_CONVERT_MONTH);
            _checkBuildTeamTime = random.Next(GameTimeConstant.TIME_CONVERT_DAY);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(_ =>
            {
                bool canCheckBuildTeam = false;
                if (_checkBuildTeamByMonth)
                {
                    if (this.GetModel<IGameModel>().Day == _checkBuildTeamDay &&
                        this.GetModel<IGameModel>().Time == _checkBuildTeamTime)
                    {
                        canCheckBuildTeam = true;
                    }
                }
                else
                {
                    if (this.GetModel<IGameModel>().Time == _checkBuildTeamTime)
                    {
                        canCheckBuildTeam = true;
                    }
                }

                if (canCheckBuildTeam)
                {
                    behaviourTree.StartTree();
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            behaviourTree.UpdateTree();
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
            team.TeamId = this.GetSystem<ITeamSystem>().AddTeam(new TeamData
            {
                generalRoleId = roleId,
                number = 1
            });
        }

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}