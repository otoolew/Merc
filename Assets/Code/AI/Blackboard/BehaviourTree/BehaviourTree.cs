using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

namespace MLIGames.Blackboard
{
    [CreateAssetMenu(menuName = "MLI Games/Blackboard/Behaviour Tree", fileName = "newBehaviourTree")]
    public class BehaviourTree : ScriptableObject
    {
        [SerializeField] private Node rootNode;
        public Node RootNode { get => rootNode; set => rootNode = value; }
        
        [SerializeField] private Node.State currentState;
        public Node.State CurrentState { get => currentState; set => currentState = value; }
        
        [SerializeField] private List<Node> nodes;
        public List<Node> Nodes { get => nodes; set => nodes = value; }
        
        [SerializeField] private Blackboard blackboard;
        public Blackboard Blackboard { get => blackboard; set => blackboard = value; }
        
        public Node.State Update() {
            if (rootNode.CurrentState == Node.State.Running) 
            {
                currentState = rootNode.Update();
            }
            return currentState;
        }

        public static List<Node> GetChildren(Node parent) {
            List<Node> children = new List<Node>();

            // if (parent is DecoratorNode decorator && decorator.child != null) {
            //     children.Add(decorator.child);
            // }
            //
            // if (parent is RootNode rootNode && rootNode.child != null) {
            //     children.Add(rootNode.child);
            // }
            //
            // if (parent is CompositeNode composite) {
            //     return composite.children;
            // }

            return children;
        }

        public static void Traverse(Node node, System.Action<Node> visiter) {
            if (node) {
                visiter.Invoke(node);
                var children = GetChildren(node);
                children.ForEach((n) => Traverse(n, visiter));
            }
        }

        public BehaviourTree Clone() {
            BehaviourTree tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, (n) => {
                tree.nodes.Add(n);
            });

            return tree;
        }

        // public void Bind(Context context) 
        // {
        //     Traverse(rootNode, node => {
        //         node.context = context;
        //         node.blackboard = blackboard;
        //     });
        // }
        // public void Bind(AIController controller) 
        // {
        //     Traverse(rootNode, node => {
        //         node.controller = controller;
        //         node.blackboard = blackboard;
        //     });
        // }
    }
}