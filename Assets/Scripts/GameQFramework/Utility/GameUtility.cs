using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace GameQFramework
{
    public class GameUtility : IGameUtility
    {
        public string TimeYToS()
        {
            DateTime currentTime = DateTime.Now;
            // 获取年-月-日-小时-分-秒
            string formattedDateTime = currentTime.ToString("yyyy-MM-dd-HH-mm-ss");
            return formattedDateTime;
        }

        public string NumToKmbt(long num, int overPower)
        {
            string str = num.ToString();
            string toKmbt;
            int lens = str.Length;

            if (lens >= overPower + 10)
            {
                toKmbt = decimal.Parse(str.Substring(0, lens - 12)).ToString("#,##0") + "T";
            }
            else if (lens >= overPower + 7)
            {
                toKmbt = decimal.Parse(str.Substring(0, lens - 9)).ToString("#,##0") + "B";
            }
            else if (lens >= overPower + 4)
            {
                toKmbt = decimal.Parse(str.Substring(0, lens - 6)).ToString("#,##0") + "M";
            }
            else if (lens >= overPower + 1)
            {
                toKmbt = decimal.Parse(str.Substring(0, lens - 3)).ToString("#,##0") + "K";
            }
            else
            {
                toKmbt = decimal.Parse(str).ToString("#,##0");
            }

            return toKmbt;
        }

        public void AnalysisJsonConfigurationTable<T>(TextAsset textAsset, Dictionary<int, T> dictionary)
            where T : BaseJsonData
        {
            T[] tData = ParseJson<T>(textAsset.text);
            foreach (T t in tData)
            {
                dictionary.TryAdd(t.ID, t);
            }
        }

        public T[] ParseJson<T>(string jsonString)
        {
            // 将JSON数据转换成指定类型的对象数组
            JsonWrapper<T> jsonWrapper = JsonUtility.FromJson<JsonWrapper<T>>(jsonString);
            return jsonWrapper?.Sheet1;
        }

        public T[] ParseJsonToList<T>(string jsonString)
        {
            T[] tData = ParseJson<T>(jsonString);
            return tData;
        }

        public void PrintArray(int[,] array)
        {
            int columns = array.GetLength(0);
            int rows = array.GetLength(1);

            for (int i = rows - 1; i >= 0; i--)
            {
                string str = "";
                for (int j = 0; j < columns; j++)
                {
                    str += array[j, i] + " ";
                }

                Debug.Log(str);
            }
        }

        public int GenerateKey(Vector2Int pos, Vector2Int length)
        {
            return pos.x * length.y + pos.y;
        }

        public int PointClosestToAPointOnALineSegment(int lineSegmentStart, int lineSegmentEnd, int point)
        {
            if (point <= lineSegmentStart)
            {
                return lineSegmentStart;
            }

            if (point >= lineSegmentEnd)
            {
                return lineSegmentEnd;
            }

            return point;
        }
    }
}