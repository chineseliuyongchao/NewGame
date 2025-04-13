#region Copyright

// **********************************************************************
// Copyright (C) 2023 ZenGame
//
// Script Name :        AdaptBgComp.cs
// Author Name :        jason
// Create Time :        2023/05/16 20:36:01
// Description :
// **********************************************************************

#endregion

using Game.GameBase;
using UnityEngine;

namespace Game.Components.Adapt
{
    /// <summary>
    /// 背景整体适配方案，针对RectTransform（大小需设置为默认大小（720，1560））做缩放适配，其所有子物体均受影响
    /// </summary>
    public class AdaptBgComp : BaseGameController
    {
        public RectTransform bg;
        public bool changeX = true;
        public bool changeY = true;

        protected override void OnControllerStart()
        {
            if (!bg)
            {
                return;
            }

            InitAdapt();
        }

        private void InitAdapt()
        {
            if (!bg)
            {
                return;
            }

            Rect canvasSize = QFramework.UIRoot.Instance.GetVisibleSize();

            var sizeDelta = bg.sizeDelta;
            float designWidth = sizeDelta.x;
            float designHeight = sizeDelta.y;

            float scaleFactorX = changeX ? canvasSize.width / designWidth : 1;
            float scaleFactorY = changeY ? canvasSize.height / designHeight : 1;
            float scaleFactor = Mathf.Max(scaleFactorX, scaleFactorY); // 使用较大的缩放避免穿帮
            scaleFactor = Mathf.Max(scaleFactor, 1); // 不能比1小
            var transform1 = this.bg.transform;
            transform1.localScale = new Vector3(scaleFactor, scaleFactor, transform1.localScale.z);
        }
    }
}