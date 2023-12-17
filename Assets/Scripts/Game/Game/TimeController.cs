using GameQFramework;
using QFramework;
using UnityEngine;

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

                if (_deltaTime >= GameConstant.CONVERT_QUARTER)
                {
                    _deltaTime = 0;
                    this.GetModel<IGameModel>().Time++;
                }

                if (this.GetModel<IGameModel>().Time >= GameConstant.CONVERT_DAY)
                {
                    this.GetModel<IGameModel>().Time = 0;
                    this.GetModel<IGameModel>().Day++;
                }

                if (this.GetModel<IGameModel>().Day >= GameConstant.CONVERT_MONTH)
                {
                    this.GetModel<IGameModel>().Day = 0;
                    this.GetModel<IGameModel>().Month++;
                }

                if (this.GetModel<IGameModel>().Month >= GameConstant.CONVERT_YEAR)
                {
                    this.GetModel<IGameModel>().Month = 0;
                    this.GetModel<IGameModel>().Year++;
                }
            }
        }
    }
}