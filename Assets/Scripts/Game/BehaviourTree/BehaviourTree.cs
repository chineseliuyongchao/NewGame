using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.BehaviourTree
{
    /// <summary>
    /// 行为树
    /// </summary>
    [CreateAssetMenu]
    public class BehaviourTree : ScriptableObject
    {
        public BaseNode rootNode;
        public BehaviourTreeState treeState = BehaviourTreeState.RUNNING;
        public List<BaseNode> nodes = new();

        /// <summary>
        /// 更新行为
        /// </summary>
        /// <returns></returns>
        public BehaviourTreeState Update()
        {
            if (rootNode.behaviourTreeState == BehaviourTreeState.RUNNING)
            {
                treeState = rootNode.Update();
            }

            return treeState;
        }

        public BaseNode CreateNode(System.Type type)
        {
            BaseNode node = CreateInstance(type) as BaseNode;
            if (node != null)
            {
                node.name = type.Name;
                node.guid = GUID.Generate().ToString();
                Undo.RecordObject(this, "Behaviour Tree (CreateNode)");
                nodes.Add(node);
                if (!Application.isPlaying)//游戏运行时不能创建节点
                {
                    AssetDatabase.AddObjectToAsset(node, this);
                }

                Undo.RegisterCreatedObjectUndo(node, "Behaviour Tree (CreateNode)");
                AssetDatabase.SaveAssets();
                return node;
            }

            return null;
        }

        public void DeleteNode(BaseNode node)
        {
            Undo.RecordObject(this, "Behaviour Tree (DeleteNode)");
            nodes.Remove(node);
            // AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(BaseNode parent, BaseNode child)
        {
            RootNode node = parent as RootNode;
            if (node)
            {
                Undo.RecordObject(node, "Behaviour Tree (AddChild)");
                node.child = child;
                EditorUtility.SetDirty(node);
            }

            BaseDecoratorNode decoratorNode = parent as BaseDecoratorNode;
            if (decoratorNode)
            {
                Undo.RecordObject(decoratorNode, "Behaviour Tree (AddChild)");
                decoratorNode.child = child;
                EditorUtility.SetDirty(decoratorNode);
            }

            BaseCompositeNode compositeNode = parent as BaseCompositeNode;
            if (compositeNode)
            {
                Undo.RecordObject(compositeNode, "Behaviour Tree (AddChild)");
                compositeNode.children.Add(child);
                EditorUtility.SetDirty(compositeNode);
            }
        }

        public void RemoveChild(BaseNode parent, BaseNode child)
        {
            RootNode node = parent as RootNode;
            if (node)
            {
                Undo.RecordObject(node, "Behaviour Tree (RemoveChild)");
                node.child = null;
                EditorUtility.SetDirty(node);
            }

            BaseDecoratorNode decoratorNode = parent as BaseDecoratorNode;
            if (decoratorNode)
            {
                Undo.RecordObject(decoratorNode, "Behaviour Tree (RemoveChild)");
                decoratorNode.child = null;
                EditorUtility.SetDirty(decoratorNode);
            }

            BaseCompositeNode compositeNode = parent as BaseCompositeNode;
            if (compositeNode)
            {
                Undo.RecordObject(compositeNode, "Behaviour Tree (RemoveChild)");
                compositeNode.children.Remove(child);
                EditorUtility.SetDirty(compositeNode);
            }
        }

        public List<BaseNode> GetChildren(BaseNode parent)
        {
            List<BaseNode> children = new List<BaseNode>();

            RootNode node = parent as RootNode;
            if (node && node.child != null)
            {
                children.Add(node.child);
            }

            BaseDecoratorNode decoratorNode = parent as BaseDecoratorNode;
            if (decoratorNode && decoratorNode.child != null)
            {
                children.Add(decoratorNode.child);
            }

            BaseCompositeNode compositeNode = parent as BaseCompositeNode;
            if (compositeNode)
            {
                return compositeNode.children;
            }

            return children;
        }

        /// <summary>
        /// 递归遍历行为树的节点，并对每个节点执行指定的操作。
        /// </summary>
        /// <param name="node">当前要遍历的节点</param>
        /// <param name="visiter">要执行的操作</param>
        private void Traverse(BaseNode node, System.Action<BaseNode> visiter)
        {
            // 如果节点不为空
            if (node)
            {
                // 执行指定操作
                visiter.Invoke(node);
                // 获取当前节点的子节点列表
                var children = GetChildren(node);
                // 递归遍历子节点并执行相同的操作
                children.ForEach(n => Traverse(n, visiter));
            }
        }

        /// <summary>
        /// 克隆整个行为树。
        /// </summary>
        /// <returns>克隆后的行为树</returns>
        public BehaviourTree Clone()
        {
            // 克隆当前行为树实例
            BehaviourTree tree = Instantiate(this);
            // 克隆根节点，并设置为新行为树的根节点
            tree.rootNode = tree.rootNode.Clone();
            // 初始化新行为树的节点列表
            tree.nodes = new List<BaseNode>();
            // 使用 Traverse 方法遍历整个行为树，将每个节点添加到新行为树的节点列表中
            Traverse(tree.rootNode, n => { tree.nodes.Add(n); });
            // 返回克隆后的行为树
            return tree;
        }
    }
}