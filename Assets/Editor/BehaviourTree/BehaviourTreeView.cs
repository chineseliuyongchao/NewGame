using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Game.BehaviourTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Editor
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits>
        {
        }

        public Action<NodeView> onNodeSelected;
        private BehaviourTree _tree;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer()); //添加内容缩放器
            this.AddManipulator(new ContentDragger()); //添加内容拖拽器
            this.AddManipulator(new SelectionDragger()); //添加选择拖拽器
            this.AddManipulator(new RectangleSelector()); //添加矩形选择器

            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviourTree/BehaviourTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            PopulateView(_tree);
            AssetDatabase.SaveAssets();
        }

        private NodeView FindNodeView(BaseNode node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        /// <summary>
        /// 展示一棵新的行为树时填充视图
        /// </summary>
        /// <param name="tree"></param>
        public void PopulateView(BehaviourTree tree)
        {
            _tree = tree;
            //移除对图形视图变化的事件处理器，以防止在重新创建图形视图时触发不必要的事件
            graphViewChanged -= OnGraphViewChanged;
            //删除当前 BehaviourTreeView 中的所有图元素
            DeleteElements(graphElements);
            //重新添加图形视图变化的事件处理器，以确保在图形视图发生变化时调用 OnGraphViewChanged 方法
            graphViewChanged += OnGraphViewChanged;
            if (tree.rootNode == null)
            {
                tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                //将行为树标记为已修改，以确保修改被保存
                EditorUtility.SetDirty(tree);
                //保存资产数据库，将修改应用到磁盘上的资产文件中
                AssetDatabase.SaveAssets();
            }

            //增加节点
            tree.nodes.ForEach(CreateNodeView);
            //增加边
            tree.nodes.ForEach(n =>
            {
                var children = _tree.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);
                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                });
            });
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            //筛选出与起始端口 (startPort) 方向不同且不属于相同节点的端口。
            return ports.ToList()
                .Where(endPort => endPort.direction != startPort.direction && endPort.node != startPort.node).ToList();
        }

        /// <summary>
        /// 处理图形视图变化的方法，例如移除和创建元素，响应用户的交互。
        /// </summary>
        /// <param name="graphViewChange">表示图形变化的对象。</param>
        /// <returns>经过处理后的 GraphViewChange 对象。</returns>
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            // 处理将要被移除的元素
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    // 如果被移除的元素是一个 NodeView
                    if (elem is NodeView nodeView)
                    {
                        // 从树中删除相应的节点
                        _tree.DeleteNode(nodeView.baseNode);
                    }

                    // 如果被移除的元素是一个 Edge
                    if (elem is Edge edge)
                    {
                        // 检查输出和输入节点是否是 NodeViews
                        if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                        {
                            // 从树中移除父节点中的子节点
                            _tree.RemoveChild(parentView.baseNode, childView.baseNode);
                        }
                    }
                });
            }

            // 处理将要被创建的边
            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    // 检查输出和输入节点是否是 NodeViews
                    if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                    {
                        // 在树中添加子节点到父节点
                        _tree.AddChild(parentView.baseNode, childView.baseNode);
                    }
                });
            }

            if (graphViewChange.movedElements != null)
            {
                nodes.ForEach(n =>
                {
                    if (n is NodeView nodeView)
                    {
                        nodeView.SortChildren();
                    }
                });
            }

            // 返回经过处理后的 GraphViewChange 对象
            return graphViewChange;
        }

        /// <summary>
        /// 创建右键行为树视图时的菜单
        /// </summary>
        /// <param name="evt"></param>
        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            {
                var types = TypeCache.GetTypesDerivedFrom<BaseActionNode>();
                MenuAppendAction(types, evt);
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<BaseConditionNode>();
                MenuAppendAction(types, evt);
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<BaseCompositeNode>();
                MenuAppendAction(types, evt);
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<BaseDecoratorNode>();
                MenuAppendAction(types, evt);
            }
        }

        private void MenuAppendAction(TypeCache.TypeCollection types, ContextualMenuPopulateEvent evt)
        {
            foreach (var type in types)
            {
                if (type.BaseType != null)
                {
                    MethodInfo methodInfo = type.GetMethod("FunctionPath"); //通过反射获取类的FunctionPath静态方法
                    string functionPath = ""; //存放FunctionPath静态方法中返回的三级目录
                    if (methodInfo != null && methodInfo.IsStatic)
                    {
                        // 如果方法是静态的，直接调用并获取返回值
                        functionPath = (string)methodInfo.Invoke(null, null);
                    }

                    evt.menu.AppendAction($"[{type.BaseType.Name}]/{functionPath}{type.Name}", _ => CreateNode(type));
                }
            }
        }

        /// <summary>
        /// 根据类型新建一个对应节点对象
        /// </summary>
        /// <param name="type"></param>
        private void CreateNode(Type type)
        {
            BaseNode node = _tree.CreateNode(type);
            CreateNodeView(node);
        }

        /// <summary>
        /// 根据节点在行为树编辑器生成一个节点UI
        /// </summary>
        /// <param name="node"></param>
        private void CreateNodeView(BaseNode node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.onNodeSelected = onNodeSelected;
            AddElement(nodeView);
        }

        public void UpdateNodeState()
        {
            nodes.ForEach(n =>
            {
                if (n is NodeView nodeView)
                {
                    nodeView.UpdateState();
                }
            });
        }
    }
}