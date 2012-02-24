using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class Graph
    {
        private readonly List<NodeInfo> nodes;

        public Graph()
        {
            this.nodes = new List<NodeInfo>();
        }

        public IEnumerable<NodeInfo> RootNodes { get { return nodes.Where(n => n.IsRootNode()); } }
        public IEnumerable<NodeInfo> LeafNodes { get { return nodes.Where(n => n.IsLeafNode()); } }

        public IList<NodeInfo> Nodes { get { return nodes; } }
    }
}
