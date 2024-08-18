using Battle.BattleBase;
using Game.GameBase;
using QFramework;
using UnityEngine;

namespace Battle
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
            if (this.GetModel<IBattleBaseModel>().TimeIsPass)
            {
                _deltaTime += Time.deltaTime;
                bool hasChange = false;

                if (_deltaTime >= GameTimeConstant.CONVERT_TIME)
                {
                    _deltaTime = 0;
                    this.GetModel<IBattleBaseModel>().NowTime.UpdateTime();
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