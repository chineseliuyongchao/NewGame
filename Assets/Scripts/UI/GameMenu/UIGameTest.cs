using Game.GameTest;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIGameTestData : UIPanelData
    {
    }

    public partial class UIGameTest : UIBase
    {
        // 测试设置面板
        private GameObject _testSettingsPanel;

        // 按钮模板
        private Button _testOptionButtonTemplate;

        // ScrollRect 组件
        private ScrollRect _scrollRect;

        // Content 容器
        private Transform _content;

        // 测试选项数据
        private struct TestOption
        {
            public string name;
            public System.Func<bool> getState;
            public System.Action<bool> setState;
            public Button button; // 关联的按钮
        }

        private TestOption[] _testOptions;

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameTestData ?? new UIGameTestData();
            base.OnInit(uiData);
            InitUI();
        }

        protected override void OnOpen(IUIData uiData = null)
        {
            mData = uiData as UIGameTestData ?? new UIGameTestData();
            base.OnOpen(uiData);
        }

        protected override void OnListenButton()
        {
            testButton.onClick.AddListener(() =>
            {
                // 切换测试设置面板的显示状态
                _testSettingsPanel.SetActive(!_testSettingsPanel.activeSelf);

                // 如果面板显示，则更新按钮状态
                if (_testSettingsPanel.activeSelf)
                {
                    UpdateButtonStates();
                }
            });
        }

        protected override void OnListenEvent()
        {
        }

        private void InitUI()
        {
            // 获取测试设置面板和按钮模板
            _testSettingsPanel = transform.Find("TestSettingsPanel").gameObject;
            _testOptionButtonTemplate = transform.Find("TestOptionButtonTemplate").GetComponent<Button>();

            // 获取 ScrollRect 和 Content
            _scrollRect = _testSettingsPanel.GetComponent<ScrollRect>();
            _content = _scrollRect.content;

            // 初始化测试选项数据
            var testModel = this.GetModel<IGameTestModel>();
            _testOptions = new[]
            {
                new TestOption
                {
                    name = "是否启用测试模式",
                    getState = () => testModel.IsTest,
                    setState = value => testModel.IsTest = value
                },
                new TestOption
                {
                    name = "是否启用固定命中率",
                    getState = () => testModel.FixedHitRateEnabled,
                    setState = value => testModel.FixedHitRateEnabled = value
                },
                new TestOption
                {
                    name = "是否禁用 AI 攻击",
                    getState = () => testModel.AINoAttack,
                    setState = value => testModel.AINoAttack = value
                },
                new TestOption
                {
                    name = "是否禁用 AI 移动",
                    getState = () => testModel.AINoMove,
                    setState = value => testModel.AINoMove = value
                },
                new TestOption
                {
                    name = "是否允许在战斗开始时摆放 AI",
                    getState = () => testModel.CanPlaceAI,
                    setState = value => testModel.CanPlaceAI = value
                },
                new TestOption
                {
                    name = "是否忽略作战意志计算",
                    getState = () => testModel.IgnoreMorale,
                    setState = value => testModel.IgnoreMorale = value
                },
                new TestOption
                {
                    name = "是否忽略疲劳值计算",
                    getState = () => testModel.IgnoreFatigue,
                    setState = value => testModel.IgnoreFatigue = value
                }
            };

            // 生成测试选项按钮
            GenerateTestOptions();
            _testSettingsPanel.SetActive(false);
            _testOptionButtonTemplate.gameObject.SetActive(false);
        }

        private void GenerateTestOptions()
        {
            // 清空现有按钮（除了模板）
            foreach (Transform child in _content)
            {
                if (child.gameObject != _testOptionButtonTemplate.gameObject)
                {
                    Destroy(child.gameObject);
                }
            }

            // 生成测试选项按钮
            for (int i = 0; i < _testOptions.Length; i++)
            {
                var button = Instantiate(_testOptionButtonTemplate, _content);
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<Text>().text = _testOptions[i].name;

                // 关联按钮到测试选项
                _testOptions[i].button = button;

                // 绑定点击事件
                int index = i; // 闭包捕获
                button.onClick.AddListener(() =>
                {
                    bool newState = !_testOptions[index].getState();
                    _testOptions[index].setState(newState);
                    if (index == 0) //如果是是否开启测试的按钮，就要把所有按钮都刷新一遍
                    {
                        UpdateButtonStates();
                    }
                    else
                    {
                        UpdateButtonColor(_testOptions[index].button, newState);
                    }
                });

                // 初始化按钮颜色
                UpdateButtonColor(button, _testOptions[i].getState());
            }
        }

        private void UpdateButtonStates()
        {
            // 更新所有按钮的状态
            foreach (var option in _testOptions)
            {
                UpdateButtonColor(option.button, option.getState());
            }
        }

        private void UpdateButtonColor(Button button, bool isActive)
        {
            var image = button.GetComponent<Image>();
            image.color = isActive ? Color.red : Color.white;
        }
    }
}