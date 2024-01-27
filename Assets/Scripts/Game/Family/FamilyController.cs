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
        private GameObject _armyPrefab;

        /// <summary>
        /// 是否每月尝试组建一支军队（默认是每天尝试一次）
        /// </summary>
        private bool _checkBuildArmyByMonth;

        /// <summary>
        /// 每月的第几天尝试组建军队
        /// </summary>
        private int _checkBuildArmyDay;

        /// <summary>
        /// 每天的第几个时辰尝试组建军队
        /// </summary>
        private int _checkBuildArmyTime;

        protected override void OnInit()
        {
            base.OnInit();
            _armyPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.ARMY);
            behaviourTree = behaviourTree.Clone();
            _familyBlackBoard = new FamilyBlackBoard
            {
                buildArmy = BuildArmy
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
            _checkBuildArmyDay = random.Next(GameTimeConstant.DAY_CONVERT_MONTH);
            _checkBuildArmyTime = random.Next(GameTimeConstant.TIME_CONVERT_DAY);
        }

        protected override void OnListenEvent()
        {
            this.RegisterEvent<ChangeTimeEvent>(_ =>
            {
                bool canCheckBuildArmy = false;
                if (_checkBuildArmyByMonth)
                {
                    if (this.GetModel<IGameModel>().Day == _checkBuildArmyDay &&
                        this.GetModel<IGameModel>().Time == _checkBuildArmyTime)
                    {
                        canCheckBuildArmy = true;
                    }
                }
                else
                {
                    if (this.GetModel<IGameModel>().Time == _checkBuildArmyTime)
                    {
                        canCheckBuildArmy = true;
                    }
                }

                if (canCheckBuildArmy)
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
        /// 组建一支军队
        /// </summary>
        /// <param name="roleId"></param>
        private void BuildArmy(int roleId)
        {
            this.GetModel<IFamilyModel>().RoleData[roleId].roleType = RoleType.GENERAL;

            GameObject armyObject = Instantiate(_armyPrefab);
            armyObject.Parent(this.GetModel<IGameModel>().PlayerArmy.transform.parent);
            int townId = this.GetModel<IFamilyModel>().RoleData[roleId].townId;
            TownCommonData townCommonData = this.GetModel<ITownModel>().TownCommonData[townId];
            armyObject.Position(new Vector3(townCommonData.TownPos[0], townCommonData.TownPos[1]));
            Army.Army army = armyObject.GetComponent<Army.Army>();
            army.ArmyId = this.GetSystem<IArmySystem>().AddArmy(new ArmyData
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