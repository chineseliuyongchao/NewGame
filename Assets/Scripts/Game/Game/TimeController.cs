using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game.Game
{
    public class TimeController : BaseGameController
    {
        /// <summary>
        /// 时间是否流逝
        /// </summary>
        private bool _isPass;

        private float _deltaTime;

        protected override void OnInit()
        {
            base.OnInit();
            _isPass = true;
            _deltaTime = 0;
        }

        protected override void OnListenEvent()
        {
        }

        private void Update()
        {
            if (_isPass)
            {
                _deltaTime += Time.deltaTime;
                bool hasChange = false;

                if (_deltaTime >= GameTimeConstant.CONVERT_QUARTER)
                {
                    _deltaTime = 0;
                    this.GetModel<IGameModel>().Time++;
                    hasChange = true;
                }

                if (this.GetModel<IGameModel>().Time >= GameTimeConstant.CONVERT_DAY)
                {
                    this.GetModel<IGameModel>().Time = 0;
                    this.GetModel<IGameModel>().Day++;
                }

                if (this.GetModel<IGameModel>().Day >= GameTimeConstant.CONVERT_MONTH)
                {
                    this.GetModel<IGameModel>().Day = 0;
                    this.GetModel<IGameModel>().Month++;
                }

                if (this.GetModel<IGameModel>().Month >= GameTimeConstant.CONVERT_YEAR)
                {
                    this.GetModel<IGameModel>().Month = 0;
                    this.GetModel<IGameModel>().Year++;
                }

                if (hasChange)
                {
                    this.SendCommand(new ChangeTimeCommand());
                }
            }
        }
    }
}