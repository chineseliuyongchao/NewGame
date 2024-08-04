namespace Fight.Utils
{
    /**
     * 这个类应该通过读表生成
     */
    public class TraitConstants
    {
        /// <summary>
        ///     鼓舞士气特质的士气加成
        /// </summary>
        public static int BoostMoraleTraitMoraleBonus = 8;

        // public static void LoadConfig(string filePath)
        // {
        //     if (File.Exists(filePath))
        //     {
        //         try
        //         {
        //             var jsonData = File.ReadAllText(filePath);
        //             var configData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        //
        //             // 获取GameConfig的所有静态字段
        //             FieldInfo[] fields = typeof(GameConfig).GetFields(BindingFlags.Static | BindingFlags.Public);
        //
        //             foreach (var field in fields)
        //             {
        //                 if (configData.ContainsKey(field.Name))
        //                 {
        //                     var value = Convert.ChangeType(configData[field.Name], field.FieldType);
        //                     field.SetValue(null, value);
        //                 }
        //             }
        //         }
        //         catch (Exception ex)
        //         {
        //             Console.WriteLine($"Error loading config: {ex.Message}");
        //         }
        //     }
        //     else
        //     {
        //         Console.WriteLine("Config file not found, using default values.");
        //     }
        // }
    }
}