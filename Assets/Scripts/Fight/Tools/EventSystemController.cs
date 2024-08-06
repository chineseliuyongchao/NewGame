using Fight.Commands;
using Fight.Game;
using QFramework;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Fight.Tools
{
    /**
     * 管理战斗场景中的所有输入（ui除外）
     */
    public class EventSystemController : MonoBehaviour, IController, IPointerClickHandler
    {
        private AStarModel _aStarModel;
        private FightGameModel _fightGameModel;

        private void Awake()
        {
            _aStarModel = this.GetModel<AStarModel>();
            _fightGameModel = this.GetModel<FightGameModel>();
        }

        public IArchitecture GetArchitecture()
        {
            return GameApp.Interface;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var cam = Camera.main;
            if (!cam) return;
            int index = _aStarModel.GetGridNodeIndexMyRule(cam.ScreenToWorldPoint(eventData.position));
            if (_fightGameModel.FightSceneArmsNameDictionary.ContainsKey(index))
            {
                this.SendCommand(new SelectArmsFocusCommand(index));
            }
        }
    }
}