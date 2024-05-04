using System;
using Game.Dialogue;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Editor.DialogueTreeEditor
{
    public class DialogueNodeView : Node
    {
        public Action<DialogueNodeView> onNodeSelected;
        public BaseDialogueNode dialogueNode;
        public Port input;
        public Port output;

        public DialogueNodeView(BaseDialogueNode dialogueNode) : base(
            "Assets/Editor/DialogueTreeEditor/DialogueNodeView.uxml")
        {
            this.dialogueNode = dialogueNode;
            // 设置 DialogueNodeView 的视图数据键（viewDataKey）为 dialogueNode 的 GUID（全局唯一标识符）
            viewDataKey = dialogueNode.guid;
            style.left = dialogueNode.position.x;
            style.top = dialogueNode.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();

            Label titleLabel = this.Q<Label>("title-label");
            titleLabel.bindingPath = "optionToThisIndex";
            titleLabel.Bind(new SerializedObject(dialogueNode));

            // 在UI中找到名为"description"的Label元素
            Label descriptionLabel = this.Q<Label>("description");
            // 将Label元素的bindingPath设置为"description"这表示Label元素将绑定到SerializedObject的"description"字段
            descriptionLabel.bindingPath = "contentIndex";
            // 使用SerializedObject进行数据绑定，将Label元素与DialogueNode对应的SerializedObject的"content"字段关联起来
            descriptionLabel.Bind(new SerializedObject(dialogueNode));
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            //告诉 Unity 的撤销系统记录 dialogueNode 对象的状态。如果在之后的操作中需要撤销，Unity 将回滚到这个记录的状态。"Dialogue Tree (Set Position)" 是记录的名称，用于标识这个操作在撤销列表中的显示。
            Undo.RecordObject(dialogueNode, "Dialogue Tree (SetPosition)");
            dialogueNode.position.x = newPos.xMin;
            dialogueNode.position.y = newPos.yMin;
            EditorUtility.SetDirty(dialogueNode);
        }

        /// <summary>
        /// 创建输入端口
        /// </summary>
        private void CreateInputPorts()
        {
            input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            input.portName = "";
            //设置 input 元素的子元素排列方向为纵向列（从上到下）。
            input.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(input);
        }

        /// <summary>
        /// 创建输出端口
        /// </summary>
        private void CreateOutputPorts()
        {
            output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            output.portName = "";
            //设置 output 元素的子元素排列方向为纵向列的反向（从下到上）。
            output.style.flexDirection = FlexDirection.ColumnReverse;
            outputContainer.Add(output);
        }

        /// <summary>
        /// 设置节点视图的样式类别，用于样式化不同类型的节点。
        /// </summary>
        private void SetupClasses()
        {
            if (dialogueNode is RootDialogueNode)
            {
                AddToClassList("root");
            }

            if (dialogueNode is PlayerDialogueNode)
            {
                AddToClassList("player");
            }

            if (dialogueNode is NpcDialogueNode)
            {
                AddToClassList("npc");
            }
        }

        public override void OnSelected()
        {
            base.OnSelected();
            onNodeSelected?.Invoke(this);
        }

        public void SortChildren()
        {
            dialogueNode.children.Sort(SortByHorizontalPosition);
        }

        private int SortByHorizontalPosition(BaseDialogueNode left, BaseDialogueNode right)
        {
            return left.position.x < right.position.x ? -1 : 1;
        }

        public void UpdateState()
        {
            RemoveFromClassList("waiting");
            RemoveFromClassList("inactive");
            RemoveFromClassList("stopped");
            if (Application.isPlaying)
            {
                switch (dialogueNode.dialogueType)
                {
                    case DialogueType.WAITING:
                        AddToClassList("waiting");
                        break;
                    case DialogueType.INACTIVE:
                        AddToClassList("inactive");
                        break;
                    case DialogueType.STOPPED:
                        AddToClassList("stopped");
                        break;
                }
            }
        }
    }
}