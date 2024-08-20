using System;
using System.Collections.Generic;
using DG.Tweening;
using Fight.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fight.Game.Arms
{
    /// <summary>
    /// 兵种的可视化组成，便于拓展修改
    /// </summary>
    public class ObjectArmsView : MonoBehaviour
    {
        private SortedList<int, Object> _list;

        public void OnInit(Transform target)
        {
            _list = new SortedList<int, Object>
            {
                { Constants.Head, target.Find("head").GetComponent<SpriteRenderer>() },
                { Constants.Body, target.Find("body").GetComponent<SpriteRenderer>() },
                { Constants.Helmet, target.Find("helmet").GetComponent<SpriteRenderer>() },
                { Constants.Armor, target.Find("armor").GetComponent<SpriteRenderer>() },
                { Constants.Weapon, target.Find("weapon").GetComponent<SpriteRenderer>() },
                { Constants.Shield, target.Find("shield").GetComponent<SpriteRenderer>() },
                { Constants.Foot, target.Find("foot").GetComponent<SpriteRenderer>() },
                { Constants.DebugText, target.Find("debugText").GetComponent<TextMesh>() }
            };
        }

        public T Find<T>(int id)
        {
            if (_list.TryGetValue(id, out var obj))
            {
                if (obj is T result) return result;

                throw new NotImplementedException("Cannot specify type.");
            }

            throw new NotImplementedException("Cannot find element.");
        }

        public void DoColor(Color color, float duration = 0.1f)
        {
            foreach (var obj in _list.Values)
                if (obj is SpriteRenderer sprite)
                    sprite.DOColor(color, duration);
        }

        public void ChangeOrderLayer(int beginIndex)
        {
            foreach (var obj in _list.Values)
            {
                if (obj is SpriteRenderer spriteRenderer)
                {
                    spriteRenderer.sortingOrder = beginIndex++;
                }
            }
        }
    }
}