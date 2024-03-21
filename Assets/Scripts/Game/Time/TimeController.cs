using GameQFramework;
using QFramework;
using UnityEngine;
using Utils.Constant;

namespace Game
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

                if (_deltaTime >= GameTimeConstant.CONVERT_TIME)
                {
                    _deltaTime = 0;
                    this.GetModel<IGameModel>().NowTime.UpdateTime();
                    hasChange = true;
                }

                if (hasChange)
                {
                    this.SendCommand(new ChangeTimeCommand());
                }
            }
        }
    }
}