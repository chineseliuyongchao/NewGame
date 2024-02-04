using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    public abstract class BaseDialogueNode : ScriptableObject
    {
        [HideInInspector] public DialogueType dialogueType;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        /// <summary>
        /// 跳转到这个节点的选项内容（如果此节点只有一个父节点则不需要）
        /// </summary>
        public string optionToThis;

        /// <summary>
        /// 此节点的对话内容
        /// </summary>
        public string content;

        /// <summary>
        /// 节点填充变量的索引
        /// </summary>
        public List<int> dialogValueIndex = new();

        /// <summary>
        /// 节点填充方法的索引
        /// </summary>
        public int dialogueActionIndex = -1;

        public List<BaseDialogueNode> children = new();

        /// <summary>
        /// 重置对话节点
        /// </summary>
        public void Resetting()
        {
            dialogueType = DialogueType.INACTIVE;
            for (int i = 0; i < children.Count; i++)
            {
                children[i].Resetting();
            }
        }

        /// <summary>
        /// 跳转到下一个对话
        /// </summary>
        /// <param name="jumpIndex"></param>
        /// <returns></returns>
        public BaseDialogueNode JumpNextDialogue(int jumpIndex)
        {
            dialogueType = DialogueType.STOPPED;
            return children.Count > jumpIndex ? children[jumpIndex] : null;
        }

        public BaseDialogueNode Clone()
        {
            BaseDialogueNode node = Instantiate(this);
            node.children = children.ConvertAll(c => c.Clone());
            return node;
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        /// <returns></returns>
        public abstract DialogueNodeType NodeType();
    }

    /// <summary>
    /// 对话节点的类型
    /// </summary>
    public enum DialogueNodeType
    {
        /// <summary>
        /// 玩家的话
        /// </summary>
        PLAYER,

        /// <summary>
        /// npc的话
        /// </summary>
        NPC,

        /// <summary>
        /// 根节点
        /// </summary>
        ROOT
    }
}