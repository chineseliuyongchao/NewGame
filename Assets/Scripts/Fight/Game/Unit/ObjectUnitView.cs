using System;
using System.Collections.Generic;
using DG.Tweening;
using Fight.Utils;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

namespace Fight.Game.Unit
{
    /// <summary>
    /// 兵种的可视化组成，便于拓展修改
    /// </summary>
    public class ObjectUnitView : MonoBehaviour
    {
        private SortedList<int, SortedList<int, Object>> _list;

        public void OnInit()
        {
            _list = new SortedList<int, SortedList<int, Object>>
            {
                { Constants.Child1, InitSortedList(transform.Find(Constants.Child1.ToString())) },
                { Constants.Child2, InitSortedList(transform.Find(Constants.Child2.ToString())) },
                { Constants.Child3, InitSortedList(transform.Find(Constants.Child3.ToString())) },
                { Constants.Child4, InitSortedList(transform.Find(Constants.Child4.ToString())) },
                { Constants.Child5, InitSortedList(transform.Find(Constants.Child5.ToString())) }
            };
        }

        private SortedList<int, Object> InitSortedList(Transform target)
        {
            SortedList<int, Object> result = new SortedList<int, Object>();
            if (target.Find("head")) result.Add(Constants.Head, target.Find("head").GetComponent<SpriteRenderer>());
            if (target.Find("body")) result.Add(Constants.Body, target.Find("body").GetComponent<SpriteRenderer>());
            if (target.Find("helmet")) result.Add(Constants.Helmet, target.Find("helmet").GetComponent<SpriteRenderer>());
            if (target.Find("armor")) result.Add(Constants.Armor, target.Find("armor").GetComponent<SpriteRenderer>());
            if (target.Find("weapon")) result.Add(Constants.Weapon, target.Find("weapon").GetComponent<SpriteRenderer>());
            if (target.Find("shield")) result.Add(Constants.Shield, target.Find("shield").GetComponent<SpriteRenderer>());
            if (target.Find("foot")) result.Add(Constants.Foot, target.Find("foot").GetComponent<SpriteRenderer>());
            return result;
        }

        public T Find<T>(int childId, int bodyId) where T : Object
        {
            if (_list.TryGetValue(childId, out var list2))
            {
                if (list2.TryGetValue(bodyId, out var obj))
                {
                    if (obj is T result) return result;
                }
            }
            return null;
        }

        public void DoColor(Color color, float duration = 0.1f)
        {
            foreach (var list2 in _list.Values)
            {
                foreach (var obj in list2.Values)
                {
                    if (obj is SpriteRenderer sprite)
                        sprite.DOColor(color, duration);
                }
            }
        }

        public void ChangeOrderLayer(int beginIndex)
        {
            foreach (var id in _list.Keys)
            {
                Transform child = transform.Find(id.ToString());
                if (child && child.TryGetComponent(out SortingGroup sortingGroup))
                {
                    sortingGroup.sortingOrder = beginIndex++;
                }
            }
        }
    }
}