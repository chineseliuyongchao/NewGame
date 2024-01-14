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
                AssetDatabase.AddObjectToAsset(node, this);
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

        public BehaviourTree Clone()
        {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            return tree;
        }
    }
}