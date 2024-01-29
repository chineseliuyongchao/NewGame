using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace Game.Dialogue
{
    /// <summary>
    /// 对话树
    /// </summary>
    [CreateAssetMenu]
    public class DialogueTree : ScriptableObject
    {
        public DialogueType dialogueType = DialogueType.INACTIVE;
        public List<BaseDialogueNode> nodes = new();
        public BaseDialogueNode rootNode;
        private BaseDialogueNode _nowNode;

        /// <summary>
        /// 开始对话
        /// </summary>
        public BaseDialogueNode StartDialogue()
        {
            dialogueType = DialogueType.WAITING;
            rootNode.Resetting();
            _nowNode = rootNode;
            return rootNode;
        }

        /// <summary>
        /// 更新对话
        /// </summary>
        /// <param name="jumpIndex"></param>
        /// <returns></returns>
        public BaseDialogueNode UpdateDialogue(int jumpIndex)
        {
            _nowNode = _nowNode.JumpNextDialogue(jumpIndex);
            _nowNode.dialogueType = DialogueType.WAITING;
            return _nowNode;
        }

#if UNITY_EDITOR
        public BaseDialogueNode CreateNode(System.Type type)
        {
            BaseDialogueNode node = CreateInstance(type) as BaseDialogueNode;
            if (node != null)
            {
                node.name = type.Name;
                node.guid = GUID.Generate().ToString();
                Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
                nodes.Add(node);
                if (!Application.isPlaying) //游戏运行时不能创建节点
                {
                    AssetDatabase.AddObjectToAsset(node, this);
                }

                Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
                AssetDatabase.SaveAssets();
                return node;
            }

            return null;
        }

        public void DeleteNode(BaseDialogueNode node)
        {
            Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
            nodes.Remove(node);
            // AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(BaseDialogueNode parent, BaseDialogueNode child)
        {
            Undo.RecordObject(parent, "Dialogue Tree (AddChild)");
            parent.children.Add(child);
            EditorUtility.SetDirty(parent);
        }

        public void RemoveChild(BaseDialogueNode parent, BaseDialogueNode child)
        {
            Undo.RecordObject(parent, "Behaviour Tree (RemoveChild)");
            parent.children.Remove(child);
            EditorUtility.SetDirty(parent);
        }
#endif

        public List<BaseDialogueNode> GetChildren(BaseDialogueNode parent)
        {
            List<BaseDialogueNode> children = parent.children;
            return children;
        }

        /// <summary>
        /// 递归遍历对话树的节点，并对每个节点执行指定的操作。
        /// </summary>
        /// <param name="node">当前要遍历的节点</param>
        /// <param name="visitor">要执行的操作</param>
        private void Traverse(BaseDialogueNode node, System.Action<BaseDialogueNode> visitor)
        {
            // 如果节点不为空
            if (node)
            {
                // 执行指定操作
                visitor.Invoke(node);
                // 获取当前节点的子节点列表
                var children = GetChildren(node);
                // 递归遍历子节点并执行相同的操作
                children.ForEach(n => Traverse(n, visitor));
            }
        }

        /// <summary>
        /// 克隆整个对话树。
        /// </summary>
        /// <returns>克隆后的对话树</returns>
        public DialogueTree Clone()
        {
            // 克隆当前对话树实例
            DialogueTree tree = Instantiate(this);
            // 克隆根节点，并设置为新对话树的根节点
            tree.rootNode = tree.rootNode.Clone();
            // 初始化新对话树的节点列表
            tree.nodes = new List<BaseDialogueNode>();
            // 使用 Traverse 方法遍历整个对话树，将每个节点添加到新对话树的节点列表中
            Traverse(tree.rootNode, n => { tree.nodes.Add(n); });
            // 返回克隆后的对话树
            return tree;
        }
    }

    /// <summary>
    /// 对话树状态
    /// </summary>
    public enum DialogueType
    {
        /// <summary>
        /// 闲置状态
        /// </summary>
        INACTIVE,

        /// <summary>
        /// 等待状态
        /// </summary>
        WAITING,

        /// <summary>
        /// 对话结束
        /// </summary>
        STOPPED
    }
}