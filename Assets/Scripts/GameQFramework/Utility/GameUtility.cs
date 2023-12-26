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
            T[] tData = JsonParser.ParseJson<T>(textAsset.text);
            foreach (T t in tData)
            {
                dictionary.TryAdd(t.ID, t);
            }
        }
    }
}