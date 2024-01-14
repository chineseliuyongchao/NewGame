using System;
using Game.BehaviourTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor
{
    public class NodeView : Node
    {
        public Action<NodeView> onNodeSelected;
        public BaseNode baseNode;
        public Port input;
        public Port output;

        public NodeView(BaseNode baseNode) : base("Assets/Editor/NodeView.uxml")
        {
            this.baseNode = baseNode;
            title = baseNode.name;
            // 设置 NodeView 的视图数据键（viewDataKey）为 BaseNode 的 GUID（全局唯一标识符）
            viewDataKey = baseNode.guid;
            style.left = baseNode.position.x;
            style.top = baseNode.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();
        }

        public sealed override string title
        {
            get => base.title;
            set => base.title = value;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            //告诉 Unity 的撤销系统记录 baseNode 对象的状态。如果在之后的操作中需要撤销，Unity 将回滚到这个记录的状态。"Behaviour Tree (Set Position)" 是记录的名称，用于标识这个操作在撤销列表中的显示。
            Undo.RecordObject(baseNode, "Behaviour Tree (SetPosition)");
            baseNode.position.x = newPos.xMin;
            baseNode.position.y = newPos.yMin;
            EditorUtility.SetDirty(baseNode);
        }

        /// <summary>
        /// 创建输入端口
        /// </summary>
        private void CreateInputPorts()
        {
            if (baseNode is BaseActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is BaseCompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is BaseDecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is RootNode)
            {
            }

            if (input != null)
            {
                input.portName = "";
                //设置 input 元素的子元素排列方向为纵向列（从上到下）。
                input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(input);
            }
        }

        /// <summary>
        /// 创建输出端口
        /// </summary>
        private void CreateOutputPorts()
        {
            if (baseNode is BaseActionNode)
            {
            }
            else if (baseNode is BaseCompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (baseNode is BaseDecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output != null)
            {
                output.portName = "";
                //设置 output 元素的子元素排列方向为纵向列的反向（从下到上）。
                output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(output);
            }
        }

        /// <summary>
        /// 设置节点视图的样式类别，用于样式化不同类型的节点。
        /// </summary>
        private void SetupClasses()
        {
            // 如果节点是基础动作节点
            if (baseNode is BaseActionNode)
            {
                AddToClassList("action"); // 将 "action" 样式类添加到节点视图中
            }
            // 如果节点是基础组合节点
            else if (baseNode is BaseCompositeNode)
            {
                AddToClassList("composite"); // 将 "composite" 样式类添加到节点视图中
            }
            // 如果节点是基础装饰节点
            else if (baseNode is BaseDecoratorNode)
            {
                AddToClassList("decorator"); // 将 "decorator" 样式类添加到节点视图中
            }
            // 如果节点是根节点
            else if (baseNode is RootNode)
            {
                AddToClassList("root"); // 将 "root" 样式类添加到节点视图中
            }
        }

        public override void OnSelected()
        {
            base.OnSelected();
            onNodeSelected?.Invoke(this);
        }

        public void SortChildren()
        {
            BaseCompositeNode compositeNode = baseNode as BaseCompositeNode;
            if (compositeNode)
            {
                compositeNode.children.Sort(SortByHorizontalPosition);
            }
        }

        private int SortByHorizontalPosition(BaseNode left, BaseNode right)
        {
            return left.position.x < right.position.x ? -1 : 1;
        }

        public void UpdateState()
        {
            RemoveFromClassList("running");
            RemoveFromClassList("failure");
            RemoveFromClassList("success");
            if (Application.isPlaying)
            {
                switch (baseNode.behaviourTreeState)
                {
                    case BehaviourTreeState.RUNNING:
                        if (baseNode.started)
                        {
                            AddToClassList("running");
                        }

                        break;
                    case BehaviourTreeState.FAILURE:
                        AddToClassList("failure");
                        break;
                    case BehaviourTreeState.SUCCESS:
                        AddToClassList("success");
                        break;
                }
            }
        }
    }
}