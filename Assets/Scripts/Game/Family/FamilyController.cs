using System;
using Game.BehaviourTree;
using GameQFramework;
using QFramework;
using Unity.VisualScripting;
using Utils.Constant;

namespace Game.Game.Family
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
            behaviourTree = behaviourTree.Clone();
            _familyBlackBoard = new FamilyBlackBoard();
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

        public BehaviourTree.BehaviourTree GetTree()
        {
            return behaviourTree;
        }
    }
}