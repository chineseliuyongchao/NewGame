using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using Excel;
using Newtonsoft.Json;
using UnityEngine;

namespace EditorUtils
{
    public abstract class Generator
    {
        // private DataSet mResultSet;
        /// <summary>
        /// 转换为Json
        /// </summary>
        public static bool Excel2Json(string excelPath, string jsonPath)
        {
            Encoding encoding = Encoding.UTF8;

            DirectoryInfo folder = new DirectoryInfo(excelPath);
            FileSystemInfo[] files = folder.GetFileSystemInfos();
            int length = files.Length;

            for (int index = 0; index < length; index++)
            {
                if (files[index].Name.EndsWith(".xlsx"))
                {
                    string childPath = files[index].FullName;
                    Debug.Log("Excel2Json:" + childPath);
                    FileStream mStream = File.Open(childPath, FileMode.Open, FileAccess.Read);
                    IExcelDataReader mExcelReader = ExcelReaderFactory.CreateOpenXmlReader(mStream);
                    DataSet mResultSet = mExcelReader.AsDataSet();
                    ConvertToJson(childPath, mResultSet, jsonPath, encoding);
                    // ConvertToJsonEx(childPath, mResultSet, _jsonPath, encoding);
                }
            }

            return true;
        }

        /// <summary>
        /// 转换为Json
        /// string  int float  double  bool
        /// </summary>
        /// <param name="childPath"></param>
        /// <param name="mResultSet"></param>
        /// <param name="jsonPath">Json文件路径</param>
        /// <param name="encoding"></param>
        // public void ConvertToJson(DataSet mResultSet, string _jsonPath, Encoding encoding, string CSharpPath)
        private static bool ConvertToJson(string childPath, DataSet mResultSet, string jsonPath, Encoding encoding)
        {
            List<string> dataName = new List<string>();
            List<string> dataType = new List<string>();
            //判断Excel文件中是否存在数据表
            if (mResultSet.Tables.Count < 1)
                return false;

            Dictionary<string, object> allTable = new Dictionary<string, object>();
            for (int x = 0; x < mResultSet.Tables.Count; x++)
            {
                //默认读取第一个数据表
                DataTable mSheet = mResultSet.Tables[x];
                string jsonName = FirstCharToUpper(mSheet.TableName);
                if (jsonName.IndexOf('#') >= 0 && jsonName.LastIndexOf('#') != jsonName.IndexOf('#'))
                {
                    Debug.Log("无法导出名 " + jsonName + "  请确定#书写正确!");
                    continue;
                }

                //判断数据表内是否存在数据
                if (mSheet.Rows.Count < 1)
                    continue;
                //读取数据表行数和列数
                int rowCount = mSheet.Rows.Count;
                int colCount = mSheet.Columns.Count;
                //准备一个列表存储整个表的数据
                List<Dictionary<string, object>> table = new List<Dictionary<string, object>>();
                List<object> tempvaluestrList = null;
                string stringReplace;
                string[] tempvaluestrArray;
                string tempfield = null;
                string temptypestring = null;
                //读取数据
                for (int i = 3; i < rowCount; i++)
                {
                    //准备一个字典存储每一行的数据
                    Dictionary<string, object> row = new Dictionary<string, object>();
                    for (int j = 0; j < colCount; j++)
                    {
                        //读取第1行数据作为表头字段
                        string field = mSheet.Rows[1][j].ToString();
                        field = field.Trim();
                        if (field != "")
                        {
                            tempfield = field;
                            if (!dataName.Contains(field))
                            {
                                dataName.Add(field);
                            }
                        }
                        else if (tempfield != "" && field == "")
                        {
                            field = tempfield;
                        }

                        string typestring = mSheet.Rows[2][j].ToString();
                        typestring = typestring.ToLower().Trim();
                        if (typestring != "")
                        {
                            temptypestring = typestring;
                            if (tempvaluestrList == null)
                            {
                                tempvaluestrList = new List<object>();
                            }
                            else
                            {
                                tempvaluestrList.Clear();
                            }

                            dataType.Add(typestring);
                        }
                        else if (typestring == "" && temptypestring != "")
                        {
                            typestring = temptypestring;
                        }

                        string valuestr = mSheet.Rows[i][j].ToString();
                        valuestr = valuestr.Trim();
                        //Key-Value对应 按类型存放
                        switch (typestring)
                        {
                            case "int":
                                if (valuestr != "")
                                {
                                    row[field] = Convert.ToInt32(valuestr);
                                }
                                else
                                {
                                    row[field] = 0;
                                }

                                break;
                            case "float":
                                if (valuestr != "")
                                {
                                    row[field] = float.Parse(valuestr);
                                }
                                else
                                {
                                    row[field] = 0;
                                }

                                break;
                            case "double":
                                if (valuestr != "")
                                {
                                    row[field] = Convert.ToDouble(valuestr);
                                }
                                else
                                {
                                    row[field] = 0;
                                }

                                break;
                            case "long":
                                if (valuestr != "")
                                {
                                    row[field] = Convert.ToInt64(valuestr);
                                }
                                else
                                {
                                    row[field] = 0;
                                }

                                break;
                            case "bool":
                                if (valuestr == "0" || valuestr == "fasle" || valuestr == "")
                                {
                                    row[field] = false;
                                }
                                else
                                {
                                    row[field] = true;
                                }

                                break;
                            case "array<int>":
                                stringReplace = valuestr.Replace("[", "");
                                stringReplace = stringReplace.Replace("]", "");
                                //将字符串，转换成字符数组
                                tempvaluestrArray = stringReplace.Split(',');

                                int[] tempintArray = new int[tempvaluestrArray.Length];
                                for (int index = 0; index < tempvaluestrArray.Length; index++)
                                {
                                    if (tempvaluestrArray.Length > 0)
                                    {
                                        if (tempvaluestrArray[index].ToString() == "")
                                        {
                                            continue;
                                        }

                                        tempintArray[index] = (Convert.ToInt32(tempvaluestrArray[index]));
                                    }
                                }

                                row[field] = tempintArray;
                                break;
                            case "array<string>":
                                stringReplace = valuestr.Replace("[", "");
                                stringReplace = stringReplace.Replace("]", "");
                                //将字符串，转换成字符数组
                                tempvaluestrArray = stringReplace.Split(',');

                                row[field] = tempvaluestrArray;
                                break;
                            case "list<int>":
                                tempvaluestrList.Add(valuestr);
                                List<int> tempintList = new List<int>();
                                for (int index = 0; index < tempvaluestrList.Count; index++)
                                {
                                    if (tempvaluestrList.Count > 0)
                                    {
                                        if (tempvaluestrList[index].ToString() == "")
                                        {
                                            continue;
                                        }

                                        tempintList.Add(Convert.ToInt32(tempvaluestrList[index]));
                                    }
                                }

                                row[field] = tempintList;
                                break;
                            case "list<string>":
                                tempvaluestrList.Add(valuestr);
                                List<object> tempstringList = new List<object>();
                                for (int index = 0; index < tempvaluestrList.Count; index++)
                                {
                                    if (tempvaluestrList.Count > 0)
                                    {
                                        if (tempvaluestrList[index].ToString() == "")
                                        {
                                            continue;
                                        }

                                        tempstringList.Add(tempvaluestrList[index]);
                                    }
                                }

                                row[field] = tempstringList;
                                break;
                            default:
                                row[field] = valuestr;
                                break;
                        }
                    }

                    //添加到表数据中
                    table.Add(row);
                }

                allTable.Add(jsonName, table);
            }

            string json = JsonConvert.SerializeObject(allTable, Newtonsoft.Json.Formatting.None);
            json = ConvertJsonString(json);
            // _jsonPath + "/" + jsonName + ".json"
            // string JsonFilePath = _jsonPath + "/" + jsonName + ".json";
            string JsonFilePath = jsonPath + Path.GetFileNameWithoutExtension(childPath) + ".json";
            //写入文件
            using (FileStream fileStream = new FileStream(JsonFilePath, FileMode.Create, FileAccess.Write))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
                {
                    textWriter.Write(json);
                }
            }

            dataName.Clear();
            dataType.Clear();
            return true;
        }

        public static bool ConvertToJsonEx(string childPath, DataSet mResultSet, string _jsonPath, Encoding encoding)
        {
            // 重新构建一个DataSet
            DataSet mNewDataSet = new DataSet();
            DataTable mNewTable = new DataTable();
            mNewDataSet.Tables.Add(mNewTable);

            //判断Excel文件中是否存在数据表
            if (mResultSet.Tables.Count < 1)
                return false;

            //默认读取第一个数据表
            DataTable mSheet = mResultSet.Tables[0];
            string jsonName = FirstCharToUpper(mSheet.TableName);

            //新构建的DataSet设置table名字
            mNewTable.TableName = jsonName;

            //判断数据表内是否存在数据
            if (mSheet.Rows.Count < 1)
                return false;

            //读取数据表行数和列数
            int rowCount = mSheet.Rows.Count;
            int colCount = mSheet.Columns.Count;

            for (int k = 0; k < colCount; k++)
            {
                //mSheet.Columns[k].ColumnName = mSheet.Rows[1][k].ToString();
                string temp = mSheet.Rows[1][k].ToString(); //属性名字_类型
                string[] tempArry = temp.Split('_');

                string pName = tempArry[0]; //属性名字
                string typeName = tempArry[1]; //类型

                //Debug.LogError(tempArry[0]+"|"+tempArry[1]);
                mSheet.Columns[k].ColumnName = pName;

                //需要什么类型自己扩展
                switch (typeName)
                {
                    case "i":
                        mNewTable.Columns.Add(new DataColumn(pName, typeof(int)));
                        break;
                    case "s":
                        mNewTable.Columns.Add(new DataColumn(pName, typeof(string)));
                        break;
                    case "f":
                        mNewTable.Columns.Add(new DataColumn(pName, typeof(float)));
                        break;
                    default: break;
                }
            }

            //思路来自：http://www.newtonsoft.com/json/help/html/SerializeDataSet.htm
            //读取数据
            for (int i = 2; i < rowCount; i++)
            {
                DataRow m_newRow = mNewTable.NewRow();
                for (int j = 0; j < colCount; j++)
                {
                    m_newRow[mSheet.Columns[j].ColumnName] = mSheet.Rows[i][j];
                }

                mNewTable.Rows.Add(m_newRow);
            }

            mNewDataSet.AcceptChanges();

            // 生成Json字符串
            string json = JsonConvert.SerializeObject(mNewDataSet, Newtonsoft.Json.Formatting.Indented);

            //写入文件
            using (FileStream fileStream =
                   new FileStream(_jsonPath + "/" + jsonName + ".json", FileMode.Create, FileAccess.Write))
            {
                using (TextWriter textWriter = new StreamWriter(fileStream, encoding))
                {
                    textWriter.Write(json);
                }
            }

            return true;
        }

        private static string FirstCharToUpper(string input)
        {
            char[] a = input.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        private static string ConvertJsonString(string str)
        {
            //格式化json字符串
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                };
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }
    }
}