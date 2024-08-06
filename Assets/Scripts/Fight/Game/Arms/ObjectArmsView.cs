using System;
using System.Collections.Generic;
using DG.Tweening;
using Fight.Utils;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Fight.Game.Arms
{
    /**
     * 这个类是为了方便兵种的可视化编辑，并且方便后续修改
     */
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
                { Constants.Foot, target.Find("foot").GetComponent<SpriteRenderer>() }
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
    }
}