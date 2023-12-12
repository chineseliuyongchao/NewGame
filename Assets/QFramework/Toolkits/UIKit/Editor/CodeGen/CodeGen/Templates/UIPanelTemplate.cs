/****************************************************************************
 * Copyright (c) 2016 ~ 2022 liangxiegame UNDER MIT License
 *
 * https://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 *
 ****************************************************************************/

namespace QFramework
{
    using System.IO;

    public class UIPanelTemplate
    {
        public static void Write(string name, string srcFilePath, string scriptNamespace,
            UIKitSettingData uiKitSettingData)
        {
            var scriptFile = srcFilePath;

            if (File.Exists(scriptFile))
            {
                return;
            }

            var writer = File.CreateText(scriptFile);

            var codeWriter = new FileCodeWriter(writer);


            var rootCode = new RootCode()
                .Using("UnityEngine")
                .Using("UnityEngine.UI")
                .Using("QFramework")
                .EmptyLine()
                .Namespace(scriptNamespace, nsScope =>
                {
                    nsScope.Class(name + "Data", "UIPanelData", false, false, classScope => { });

                    nsScope.Class(name, "UIBase", true, false, classScope =>
                    {
                        classScope.CustomScope("protected override void OnInit(IUIData uiData = null)", false,
                            function =>
                            {
                                function.Custom(string.Format("mData = uiData as {0} ?? new {0}();", (name + "Data")));
                                function.Custom("// please add init code here");
                                function.Custom("base.OnInit(uiData);");
                            });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnOpen(IUIData uiData = null)", false,
                            function =>
                            {
                                function.Custom(string.Format("mData = uiData as {0} ?? new {0}();", (name + "Data")));
                                function.Custom("// please add open code here");
                                function.Custom("base.OnOpen(uiData);");
                            });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnShow()", false,
                            function => { function.Custom("base.OnShow();"); });
                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnHide()", false,
                            function => { function.Custom("base.OnHide();"); });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnClose()", false,
                            function => { function.Custom("base.OnClose();"); });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnListenButton()", false,
                            function => { });

                        classScope.EmptyLine();
                        classScope.CustomScope("protected override void OnListenEvent()", false,
                            function => { });
                    });
                });

            rootCode.Gen(codeWriter);
            codeWriter.Dispose();
        }
    }
}