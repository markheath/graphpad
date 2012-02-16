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

        public IList<NodeInfo> Nodes { get { return nodes; } }
    }
}
