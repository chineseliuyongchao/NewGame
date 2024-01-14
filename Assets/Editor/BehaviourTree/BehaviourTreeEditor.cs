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
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            _treeView = root.Q<BehaviourTreeView>();
            _inspectorView = root.Q<InspectorView>();
            _treeView.onNodeSelected = OnNodeSelectionChanged;
            OnSelectionChange();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateCHanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateCHanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateCHanged;
        }

        private void OnPlayModeStateCHanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        private void OnSelectionChange()
        {
            BehaviourTree tree = Selection.activeObject as BehaviourTree;
            if (!tree)
            {
                if (Selection.activeGameObject)
                {
                    IGetBehaviourTree getTree = Selection.activeGameObject.GetComponent<IGetBehaviourTree>();
                    if (getTree != null)
                    {
                        tree = getTree.GetTree();
                    }
                }
            }

            if (tree)
            {
                if (Application.isPlaying)
                {
                    _treeView.PopulateView(tree);
                }
                else if (AssetDatabase.CanOpenAssetInEditor(tree.GetInstanceID()))
                {
                    _treeView.PopulateView(tree);
                }
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