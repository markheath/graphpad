using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class NodeInfo
    {
        public NodeInfo()
        {
            this.Connections = new List<Connection>();
        }

        public void AddChild(NodeInfo child)
        {
            AddConnection(child, RelationshipType.Child);
        }

        private void AddConnection(NodeInfo connectTo, RelationshipType relationshipType)
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
        public IEnumerable<NodeInfo> Parents { get { return Connections.Where(x => x.Relationship == RelationshipType.Parent).Select(x => x.TargetNode); } }
        public IEnumerable<NodeInfo> Children { get { return Connections.Where(x => x.Relationship == RelationshipType.Child).Select(x => x.TargetNode); } }
        private IList<Connection> Connections { get; set; }
        public override string ToString()
        {
            return String.Format("Node {0}", Name);
        }
    }
}
