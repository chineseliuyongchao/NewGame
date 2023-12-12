using System.Collections.Generic;
using GameQFramework;
using UnityEngine;

namespace Game.Achieve
{
    /// <summary>
    /// 成就系统
    /// </summary>
    public class AchieveController : BaseGameController
    {
        /// <summary>
        /// 每5秒检测一次成就
        /// </summary>
        private const int TIME_DELTA_TIME = 5;

        private float _time;

        private List<BaseAchieve> _achieves;

        private void Start()
        {
            _achieves = new List<BaseAchieve>();
            _achieves.Add(new StayTwoTimeAchieve());
        }

        private void Update()
        {
            _time += Time.deltaTime;
            if (_time >= TIME_DELTA_TIME)
            {
                for (int i = 0; i < _achieves.Count; i++)
                {
                    if (_achieves[i].CheckFinish())
                    {
                        _achieves[i].OnFinish();
                        _achieves.Remove(_achieves[i]);
                    }
                }
            }
        }
    }
}