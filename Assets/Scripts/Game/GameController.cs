using System;
using System.Collections.Generic;
using Game.Family;
using GameQFramework;
using QFramework;
using UI;
using UnityEngine;
using Utils.Constant;

namespace Game
{
    public class GameController : BaseGameController
    {
        private GameObject _meshPrefab;
        private GameObject _familyPrefab;
        private List<FamilyController> _families;

        protected override void OnInit()
        {
            base.OnInit();
            _meshPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.MESH_PREFAB);
            _familyPrefab = resLoader.LoadSync<GameObject>(GamePrefabConstant.FAMILY);
            _families = new List<FamilyController>();
        }

        protected override void OnControllerStart()
        {
            base.OnControllerStart();
            // //将寻路网格显示出来
            // PathfindingMap map = this.GetModel<IMapModel>().Map;
            // int num = 0;
            // map.MapData.ForEach((i, j, value) =>
            // {
            //     if (map.CheckPass(value) && value.nodeRect.x == i && value.nodeRect.y == j)
            //     {
            //         GameObject mesh = Instantiate(meshPrefab, transform);
            //         mesh.name = this.GetUtility<IGameUtility>().GenerateKey(new Vector2Int(i, j), new Vector2Int(1000,1000))
            //             .ToString();
            //         mesh.transform.localScale = new Vector3(value.nodeRect.width, value.nodeRect.height);
            //         // ReSharper disable once PossibleLossOfFraction
            //         mesh.transform.position = new Vector3(
            //             (i * MapConstant.GRID_SIZE - 960 + value.nodeRect.width * MapConstant.GRID_SIZE / 2) /
            //             (float)MapConstant.MAP_PIXELS_PER_UNIT,
            //             // ReSharper disable once PossibleLossOfFraction
            //             (j * MapConstant.GRID_SIZE - 540 + value.nodeRect.height * MapConstant.GRID_SIZE / 2) /
            //             (float)MapConstant.MAP_PIXELS_PER_UNIT);
            //         num++;
            //     }
            // });
            // Debug.Log("网格数量：" + num);

            List<int> familyKey = new List<int>(this.GetModel<IFamilyModel>().FamilyData.Keys);
            for (int i = 0; i < familyKey.Count; i++)
            {
                GameObject family = Instantiate(_familyPrefab, transform);
                FamilyController familyController = family.GetComponent<FamilyController>();
                familyController.Init(familyKey[i]);
                _families.Add(familyController);
            }

            UIKit.OpenPanel<UIGameLobby>();

            List<string> dialogueValue = new List<string>();
            List<Action> dialogueAction = new List<Action>();
            UIKit.OpenPanel<UIDialogue>(new UIDialogueData(DialogueConstant.NEW_DIALOGUE_TREE, dialogueValue,
                dialogueAction));
        }

        private void Update()
        {
            if (!this.GetModel<IGameModel>().HasShowDialog) //没有打开弹窗时才能监听按键事件
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UIKit.OpenPanel<UIGameLobbyMenu>();
                }

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    this.SendCommand(new TimePassCommand(!this.GetModel<IGameModel>().TimeIsPass));
                }
            }
        }
    }
}