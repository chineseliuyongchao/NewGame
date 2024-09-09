using UnityEngine;
using UnityEditor;
using UnityEditor.IMGUI.Controls;

/// <summary>
/// 带数据的TreeViewItem
/// </summary>
public class AssetViewItem : TreeViewItem
{
    public ReferenceFinderData.AssetDescription data;
}

//资源引用树
public class AssetTreeView : TreeView
{
    /// <summary>
    /// 图标宽度
    /// </summary>
    const float K_ICON_WIDTH = 18f;

    /// <summary>
    /// 列表高度
    /// </summary>
    const float K_ROW_HEIGHTS = 20f;

    public AssetViewItem assetRoot;

    private GUIStyle _stateGUIStyle = new() { richText = true, alignment = TextAnchor.MiddleCenter };

    /// <summary>
    /// 列信息
    /// </summary>
    enum MyColumns
    {
        NAME,
        PATH,
        STATE,
    }

    public AssetTreeView(TreeViewState state, MultiColumnHeader multicolumnHeader) : base(state, multicolumnHeader)
    {
        rowHeight = K_ROW_HEIGHTS;
        columnIndexForTreeFoldouts = 0;
        showAlternatingRowBackgrounds = true;
        showBorder = false;
        customFoldoutYOffset =
            (K_ROW_HEIGHTS - EditorGUIUtility.singleLineHeight) *
            0.5f; // center foldout in the row since we also center content. See RowGUI
        extraSpaceBeforeIconAndLabel = K_ICON_WIDTH;
    }

    /// <summary>
    /// 响应右击事件
    /// </summary>
    /// <param name="id"></param>
    protected override void ContextClickedItem(int id)
    {
        SetExpanded(id, !IsExpanded(id));
    }
    
    /// <summary>
    /// 响应双击事件
    /// </summary>
    /// <param name="id"></param>
    protected override void DoubleClickedItem(int id)
    {
        var item = (AssetViewItem)FindItem(id, rootItem);
        //在ProjectWindow中高亮双击资源
        if (item != null)
        {
            var assetObject = AssetDatabase.LoadAssetAtPath(item.data.path, typeof(UnityEngine.Object));
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = assetObject;
            EditorGUIUtility.PingObject(assetObject);
        }
    }

    /// <summary>
    /// 生成ColumnHeader
    /// </summary>
    /// <param name="treeViewWidth"></param>
    /// <returns></returns>
    public static MultiColumnHeaderState CreateDefaultMultiColumnHeaderState(float treeViewWidth)
    {
        var columns = new[]
        {
            //图标+名称
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Name"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 200,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = false,
                canSort = false
            },
            //路径
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("Path"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 360,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = false,
                canSort = false
            },
            //状态
            new MultiColumnHeaderState.Column
            {
                headerContent = new GUIContent("State"),
                headerTextAlignment = TextAlignment.Center,
                sortedAscending = false,
                width = 60,
                minWidth = 60,
                autoResize = false,
                allowToggleVisibility = true,
                canSort = false
            },
        };
        var state = new MultiColumnHeaderState(columns);
        return state;
    }

    protected override TreeViewItem BuildRoot()
    {
        return assetRoot;
    }

    protected override void RowGUI(RowGUIArgs args)
    {
        var item = (AssetViewItem)args.item;
        for (int i = 0; i < args.GetNumVisibleColumns(); ++i)
        {
            CellGUI(args.GetCellRect(i), item, (MyColumns)args.GetColumn(i), ref args);
        }
    }

    /// <summary>
    /// 绘制列表中的每项内容
    /// </summary>
    /// <param name="cellRect"></param>
    /// <param name="item"></param>
    /// <param name="column"></param>
    /// <param name="args"></param>
    void CellGUI(Rect cellRect, AssetViewItem item, MyColumns column, ref RowGUIArgs args)
    {
        CenterRectUsingSingleLineHeight(ref cellRect);
        switch (column)
        {
            case MyColumns.NAME:
            {
                var iconRect = cellRect;
                iconRect.x += GetContentIndent(item);
                iconRect.width = K_ICON_WIDTH;
                if (iconRect.x < cellRect.xMax)
                {
                    var icon = GetIcon(item.data.path);
                    if (icon != null)
                        GUI.DrawTexture(iconRect, icon, ScaleMode.ScaleToFit);
                }

                args.rowRect = cellRect;
                base.RowGUI(args);
            }
                break;
            case MyColumns.PATH:
            {
                GUI.Label(cellRect, item.data.path);
            }
                break;
            case MyColumns.STATE:
            {
                GUI.Label(cellRect, ReferenceFinderData.GetInfoByState(item.data.state), _stateGUIStyle);
            }
                break;
        }
    }

    /// <summary>
    /// 根据资源信息获取资源图标
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Texture2D GetIcon(string path)
    {
        Object obj = AssetDatabase.LoadAssetAtPath(path, typeof(Object));
        if (obj != null)
        {
            Texture2D icon = AssetPreview.GetMiniThumbnail(obj);
            if (icon == null)
                icon = AssetPreview.GetMiniTypeThumbnail(obj.GetType());
            return icon;
        }

        return null;
    }
}