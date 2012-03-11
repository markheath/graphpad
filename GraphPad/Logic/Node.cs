using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    public class Node : Entity
    {
        public Node()
        {
            this.Connections = new List<Connection>();
        }

        public void AddChild(Node child)
        {
            if (IsAncestor(child))
            {
                string message = String.Format("Invalid DAG {0} cannot be a child and ancestor of {1}", child.Name, this.Name);
                throw new InvalidOperationException(message);
            }
            AddConnection(child, RelationshipType.Child);
        }

        public bool IsAncestor(Node test)
        {
            return IsAncestor(this.Parents, test);
        }

        private static bool IsAncestor(IEnumerable<Node> nodes, Node test)
        {
            foreach (var node in nodes)
            {
                if (node == test || IsAncestor(node.Parents, test))
                    return true;
            }
            return false;
        }

        private void AddConnection(Node connectTo, RelationshipType relationshipType)
        {
            if (connectTo == null)
                throw new ArgumentNullException();
            if (connectTo == this)
                throw new ArgumentException("Don't support connection to self");

            var existingConnection = Connections.Where(c => c.TargetNode == connectTo).FirstOrDefault();
            if (existingConnection != null && existingConnection.Relationship == relationshipType)
            {
                // avoid infinite loop
                return;
            }
            else if (existingConnection != null)
            {
                throw new ArgumentException("Already connected to this node");
            }

            this.Connections.Add(new Connection() { TargetNode = connectTo, Relationship = relationshipType });
            connectTo.AddConnection(this, GetReciprocalRelationship(relationshipType));
        }

        private RelationshipType GetReciprocalRelationship(RelationshipType relationshipType)
        {
            if (relationshipType == RelationshipType.Child)
                return RelationshipType.Parent;
            else if (relationshipType == RelationshipType.Parent)
                return RelationshipType.Child;
            else
                return RelationshipType.Peer;
        }   

        public string Name { get; set; }
        
        public IEnumerable<Node> Parents { get { return Connections.Where(x => x.Relationship == RelationshipType.Parent).Select(x => x.TargetNode); } }
        public IEnumerable<Node> Children { get { return Connections.Where(x => x.Relationship == RelationshipType.Child).Select(x => x.TargetNode); } }
        
        private IList<Connection> Connections { get; set; }
        
        public override string ToString()
        {
            return String.Format("Node {0}", Name);
        }

        public bool IsRootNode()
        {
            return !Parents.Any();
        }

        public bool IsLeafNode()
        {
            return !Children.Any();
        }

        public IDictionary<Node,int> FindLongestDistanceToAllAncestors()
        {
            var ancestorDistances = new List<NodeAndDistance>();
            FindDistanceToAncestors(ancestorDistances, 1, this.Parents);

            var nodes = ancestorDistances.Select(x => x.Node).Distinct();
            Func<Node, int> valueSelector = n => ancestorDistances.Where(x => x.Node == n).Select(x => x.Distance).Max();
            return nodes.ToDictionary(n => n, valueSelector);
        }

        // n.b. same ancestor can appear multiple times at different distances
        private void FindDistanceToAncestors(List<NodeAndDistance> ancestors, int distanceSoFar, IEnumerable<Node> nodes)
        {
            foreach (var node in nodes)
            {
                ancestors.Add(new NodeAndDistance() { Node = node, Distance = distanceSoFar });
                FindDistanceToAncestors(ancestors, distanceSoFar + 1, node.Parents);
            }
        }
    }

    class NodeAndDistance
    {
        public Node Node { get; set; }
        public int Distance { get; set; }
    }

}
