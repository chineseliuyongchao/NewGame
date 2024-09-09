using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

public class ReferenceFinderWindow : EditorWindow
{
    /// <summary>
    /// 依赖模式的key
    /// </summary>
    const string IS_DEPEND_PREF_KEY = "ReferenceFinderData_IsDepend";

    /// <summary>
    /// 是否需要更新信息状态的key
    /// </summary>
    const string NEED_UPDATE_STATE_PREF_KEY = "ReferenceFinderData_needUpdateState";

    private static ReferenceFinderData _data = new();
    private static bool _initializedData;

    private bool _isDepend;
    private bool _needUpdateState = true;

    private bool _needUpdateAssetTree;

    private bool _initializedGUIStyle;

    /// <summary>
    /// 工具栏按钮样式
    /// </summary>
    private GUIStyle _toolbarButtonGUIStyle;

    /// <summary>
    /// 工具栏样式
    /// </summary>
    private GUIStyle _toolbarGUIStyle;

    /// <summary>
    /// 选中资源列表
    /// </summary>
    private List<string> _selectedAssetGuid = new();

    private AssetTreeView _mAssetTreeView;

    [SerializeField] private TreeViewState m_TreeViewState;

    /// <summary>
    /// 查找资源引用信息
    /// </summary>
    [MenuItem("Assets/Find References In Project %#&f", false, 25)]
    public static void FindRef()
    {
        InitDataIfNeeded();
        OpenWindow();
        ReferenceFinderWindow window = GetWindow<ReferenceFinderWindow>();
        window.UpdateSelectedAssets();
    }

    public static void FindRef(List<string> selectedItems)
    {
        InitDataIfNeeded();
        OpenWindow();
        ReferenceFinderWindow window = GetWindow<ReferenceFinderWindow>();
        window.UpdateSelectedAssets(selectedItems);
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    [MenuItem("Window/Reference Finder", false, 1000)]
    static void OpenWindow()
    {
        ReferenceFinderWindow window = GetWindow<ReferenceFinderWindow>();
        window.wantsMouseMove = false;
        window.titleContent = new GUIContent("Ref Finder");
        window.Show();
        window.Focus();
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public static void InitDataIfNeeded()
    {
        if (!_initializedData)
        {
            //初始化数据
            if (!_data.ReadFromCache())
            {
                _data.CollectDependenciesInfo();
            }

            _initializedData = true;
        }
    }

    /// <summary>
    /// 初始化GUIStyle
    /// </summary>
    void InitGUIStyleIfNeeded()
    {
        if (!_initializedGUIStyle)
        {
            _toolbarButtonGUIStyle = new GUIStyle("ToolbarButton");
            _toolbarGUIStyle = new GUIStyle("Toolbar");
            _initializedGUIStyle = true;
        }
    }

    /// <summary>
    /// 更新选中资源列表
    /// </summary>
    /// <param name="selectedItems"></param>
    private void UpdateSelectedAssets(List<string> selectedItems)
    {
        _selectedAssetGuid.Clear();
        foreach (var selectedItem in selectedItems)
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(selectedItem);
            string path = AssetDatabase.GetAssetPath(obj);
            //如果是文件夹
            if (Directory.Exists(path))
            {
                string[] folder = new string[] { path };
                //将文件夹下所有资源作为选择资源
                string[] guids = AssetDatabase.FindAssets(null, folder);
                foreach (var guid in guids)
                {
                    if (!_selectedAssetGuid.Contains(guid) &&
                        !Directory.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                    {
                        _selectedAssetGuid.Add(guid);
                    }
                }
            }
            //如果是文件资源
            else
            {
                string guid = AssetDatabase.AssetPathToGUID(path);
                _selectedAssetGuid.Add(guid);
            }
        }

        _needUpdateAssetTree = true;
    }

    /// <summary>
    /// 更新选中资源列表
    /// </summary>
    private void UpdateSelectedAssets()
    {
        _selectedAssetGuid.Clear();
        foreach (var obj in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(obj);
            //如果是文件夹
            if (Directory.Exists(path))
            {
                string[] folder = new string[] { path };
                //将文件夹下所有资源作为选择资源
                string[] guids = AssetDatabase.FindAssets(null, folder);
                foreach (var guid in guids)
                {
                    if (!_selectedAssetGuid.Contains(guid) &&
                        !Directory.Exists(AssetDatabase.GUIDToAssetPath(guid)))
                    {
                        _selectedAssetGuid.Add(guid);
                    }
                }
            }
            //如果是文件资源
            else
            {
                string guid = AssetDatabase.AssetPathToGUID(path);
                _selectedAssetGuid.Add(guid);
            }
        }

        _needUpdateAssetTree = true;
    }

    /// <summary>
    /// 通过选中资源列表更新TreeView
    /// </summary>
    private void UpdateAssetTree()
    {
        if (_needUpdateAssetTree && _selectedAssetGuid.Count != 0)
        {
            var root = SelectedAssetGuidToRootItem(_selectedAssetGuid);
            if (_mAssetTreeView == null)
            {
                //初始化TreeView
                if (m_TreeViewState == null)
                    m_TreeViewState = new TreeViewState();
                var headerState = AssetTreeView.CreateDefaultMultiColumnHeaderState(position.width);
                var multiColumnHeader = new MultiColumnHeader(headerState);
                _mAssetTreeView = new AssetTreeView(m_TreeViewState, multiColumnHeader);
            }

            _mAssetTreeView.assetRoot = root;
            _mAssetTreeView.CollapseAll();
            _mAssetTreeView.Reload();
            _needUpdateAssetTree = false;
        }
    }

    private void OnEnable()
    {
        _isDepend = PlayerPrefs.GetInt(IS_DEPEND_PREF_KEY, 0) == 1;
        _needUpdateState = PlayerPrefs.GetInt(NEED_UPDATE_STATE_PREF_KEY, 1) == 1;
    }

    private void OnGUI()
    {
        InitGUIStyleIfNeeded();
        DrawOptionBar();
        UpdateAssetTree();
        if (_mAssetTreeView != null)
        {
            //绘制Treeview
            _mAssetTreeView.OnGUI(new Rect(0, _toolbarGUIStyle.fixedHeight, position.width,
                position.height - _toolbarGUIStyle.fixedHeight));
        }
    }

    /// <summary>
    /// 绘制上条
    /// </summary>
    public void DrawOptionBar()
    {
        EditorGUILayout.BeginHorizontal(_toolbarGUIStyle);
        //刷新数据
        if (GUILayout.Button("Refresh Data", _toolbarButtonGUIStyle))
        {
            _data.CollectDependenciesInfo();
            _needUpdateAssetTree = true;
            EditorGUIUtility.ExitGUI();
        }

        //修改模式
        bool preIsDepend = _isDepend;
        _isDepend = GUILayout.Toggle(_isDepend, _isDepend ? "Model(Depend)" : "Model(Reference)", _toolbarButtonGUIStyle,
            GUILayout.Width(100));
        if (preIsDepend != _isDepend)
        {
            OnModelSelect();
        }

        //是否需要更新状态
        bool preNeedUpdateState = _needUpdateState;
        _needUpdateState = GUILayout.Toggle(_needUpdateState, "Need Update State", _toolbarButtonGUIStyle);
        if (preNeedUpdateState != _needUpdateState)
        {
            PlayerPrefs.SetInt(NEED_UPDATE_STATE_PREF_KEY, _needUpdateState ? 1 : 0);
        }

        GUILayout.FlexibleSpace();

        //扩展
        if (GUILayout.Button("Expand", _toolbarButtonGUIStyle))
        {
            if (_mAssetTreeView != null) _mAssetTreeView.ExpandAll();
        }

        //折叠
        if (GUILayout.Button("Collapse", _toolbarButtonGUIStyle))
        {
            if (_mAssetTreeView != null) _mAssetTreeView.CollapseAll();
        }

        EditorGUILayout.EndHorizontal();
    }

    private void OnModelSelect()
    {
        _needUpdateAssetTree = true;
        PlayerPrefs.SetInt(IS_DEPEND_PREF_KEY, _isDepend ? 1 : 0);
    }


    /// <summary>
    /// 生成root相关
    /// </summary>
    private HashSet<string> _updatedAssetSet = new();

    /// <summary>
    /// 通过选择资源列表生成TreeView的根节点
    /// </summary>
    /// <param name="selectedAssetGuid"></param>
    /// <returns></returns>
    private AssetViewItem SelectedAssetGuidToRootItem(List<string> selectedAssetGuid)
    {
        _updatedAssetSet.Clear();
        int elementCount = 0;
        var root = new AssetViewItem { id = elementCount, depth = -1, displayName = "Root", data = null };
        int depth = 0;
        var stack = new Stack<string>();
        foreach (var childGuid in selectedAssetGuid)
        {
            var child = CreateTree(childGuid, ref elementCount, depth, stack);
            if (child != null)
                root.AddChild(child);
        }

        _updatedAssetSet.Clear();
        return root;
    }

    /// <summary>
    /// 通过每个节点的数据生成子节点
    /// </summary>
    /// <param name="guid"></param>
    /// <param name="elementCount"></param>
    /// <param name="depth"></param>
    /// <param name="stack"></param>
    /// <returns></returns>
    private AssetViewItem CreateTree(string guid, ref int elementCount, int depth, Stack<string> stack)
    {
        if (stack.Contains(guid))
            return null;

        stack.Push(guid);
        if (_needUpdateState && !_updatedAssetSet.Contains(guid))
        {
            _data.UpdateAssetState(guid);
            _updatedAssetSet.Add(guid);
        }

        ++elementCount;
        var referenceData = _data.assetDict[guid];
        var root = new AssetViewItem
            { id = elementCount, displayName = referenceData.name, data = referenceData, depth = depth };
        var childGuids = _isDepend ? referenceData.dependencies : referenceData.references;
        foreach (var childGuid in childGuids)
        {
            var child = CreateTree(childGuid, ref elementCount, depth + 1, stack);
            if (child != null)
                root.AddChild(child);
        }

        stack.Pop();
        return root;
    }
}