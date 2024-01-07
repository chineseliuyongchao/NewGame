using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game.Game
{
    public class TimeController : BaseGameController
    {
        private float _deltaTime;

        protected override void OnInit()
        {
            base.OnInit();
            _deltaTime = 0;
        }

        private void Update()
        {
            if (this.GetModel<IGameModel>().TimeIsPass)
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