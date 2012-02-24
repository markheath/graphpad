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
        
        public IEnumerable<NodeInfo> FindLongestPath()
        {
            return FindLongest(new List<NodeInfo>(), RootNodes);
        }

        private List<NodeInfo> FindLongest(List<NodeInfo> pathSoFar, IEnumerable<NodeInfo> nodesToTest)
        {
            var longest = pathSoFar;
            foreach (var node in nodesToTest)
            {
                var newPath = new List<NodeInfo>();
                newPath.AddRange(pathSoFar);
                newPath.Add(node);
                var testPath = FindLongest(newPath, node.Children);
                if (testPath.Count > pathSoFar.Count)
                    longest = testPath;
            }
            return longest;
        }
    }
}
