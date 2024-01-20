using Game.BehaviourTree;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class BehaviourTreeEditor : EditorWindow
    {
        private BehaviourTreeView _treeView;
        private InspectorView _inspectorView;
        private IMGUIContainer _blackBoardView;

        private SerializedProperty _blackBoardProperty;
        private SerializedObject _treeObject;

        [MenuItem("Tools/BehaviourTreeEditor")]
        public static void OpenWidow()
        {
            BehaviourTreeEditor wnd = GetWindow<BehaviourTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviourTreeEditor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            // 检查打开的对象是否为 BehaviourTree 类型
            if (Selection.activeObject is BehaviourTree)
            {
                // 如果是，调用 OpenWidow 方法打开自定义窗口
                OpenWidow();
                return true;
            }

            return false;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviourTreeView>();
            _inspectorView = root.Q<InspectorView>();
            _blackBoardView = root.Q<IMGUIContainer>();
            // 为黑板视图添加自定义的 GUI 处理器
            _blackBoardView.onGUIHandler = () =>
            {
                if (_treeObject != null && _blackBoardView != null)
                {
                    // 在每一帧绘制 GUI 之前，确保 SerializedObject 已更新
                    _treeObject.Update();
                    if (_blackBoardProperty != null)
                    {
                        //此方法无法遍历到子属性，暂时未发现原因
                        EditorGUILayout.PropertyField(_blackBoardProperty, true);
                    }

                    // 将修改后的属性值应用到 SerializedObject
                    _treeObject.ApplyModifiedProperties();
                }
            };
            _treeView.onNodeSelected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnEnable()
        {
            // 在脚本启用时，先移除之前注册的回调函数，避免重复注册
            EditorApplication.playModeStateChanged -= OnPlayModeStateCHanged;
            // 注册编辑器播放模式状态变化的回调函数
            EditorApplication.playModeStateChanged += OnPlayModeStateCHanged;
        }

        private void OnDisable()
        {
            // 在脚本禁用时移除编辑器播放模式状态变化的回调函数
            EditorApplication.playModeStateChanged -= OnPlayModeStateCHanged;
        }

        /// <summary>
        /// 编辑器播放模式状态变化时的回调函数
        /// </summary>
        /// <param name="obj"></param>
        private void OnPlayModeStateCHanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    // 当进入编辑模式时，调用选择变化的方法
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    // 当退出编辑模式时，可以在这里执行相应的操作
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    // 当进入播放模式时，调用选择变化的方法
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    // 当退出播放模式时，可以在这里执行相应的操作
                    break;
            }
        }

        /// <summary>
        /// 当选择发生变化时的回调函数
        /// </summary>
        private void OnSelectionChange()
        {
            // 尝试获取当前选中的对象是否为 BehaviourTree 类型
            BehaviourTree tree = Selection.activeObject as BehaviourTree;
            // 如果不是 BehaviourTree，尝试从选中的游戏对象获取 IGetBehaviourTree 接口
            if (!tree && Selection.activeGameObject)
            {
                IGetBehaviourTree getTree = Selection.activeGameObject.GetComponent<IGetBehaviourTree>();
                // 如果接口不为空，获取 BehaviourTree 实例
                if (getTree != null)
                {
                    tree = getTree.GetTree();
                }
            }

            // 如果成功获取到 BehaviourTree 实例
            if (tree)
            {
                // 如果当前处于播放模式，或者 BehaviourTree 可以在编辑器中打开
                if (Application.isPlaying || AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    // 刷新行为树视图
                    _treeView?.PopulateView(tree);
                }
            }

            if (tree != null)
            {
                _treeObject = new SerializedObject(tree);
                _blackBoardProperty = _treeObject.FindProperty("blackboard");
            }
        }

        private void OnNodeSelectionChanged(NodeView nodeView)
        {
            _inspectorView.UpdateSelection(nodeView);
        }

        private void OnInspectorUpdate()
        {
            _treeView?.UpdateNodeState();
        }
    }
}