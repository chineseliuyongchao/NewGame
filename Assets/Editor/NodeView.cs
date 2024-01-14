using System;
using Game.BehaviourTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Editor
{
    public class NodeView : Node
    {
        public Action<NodeView> onNodeSelected;
        public BaseNode baseNode;
        public Port input;
        public Port output;

        public NodeView(BaseNode baseNode)
        {
            this.baseNode = baseNode;
            title = baseNode.name;
            // 设置 NodeView 的视图数据键（viewDataKey）为 BaseNode 的 GUID（全局唯一标识符）
            viewDataKey = baseNode.guid;
            style.left = baseNode.position.x;
            style.top = baseNode.position.y;

            CreateInputPorts();
            CreateOutputPorts();
        }

        public sealed override string title
        {
            get => base.title;
            set => base.title = value;
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            baseNode.position.x = newPos.xMin;
            baseNode.position.y = newPos.yMin;
        }

        /// <summary>
        /// 创建输入端口
        /// </summary>
        private void CreateInputPorts()
        {
            if (baseNode is BaseActionNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is BaseCompositeNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is BaseDecoratorNode)
            {
                input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is RootNode)
            {
            }

            if (input != null)
            {
                input.portName = "";
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
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (baseNode is BaseDecoratorNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (baseNode is RootNode)
            {
                output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output != null)
            {
                output.portName = "";
                outputContainer.Add(output);
            }
        }

        public override void OnSelected()
        {
            base.OnSelected();
            onNodeSelected?.Invoke(this);
        }
    }
}