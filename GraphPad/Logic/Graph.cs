using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class Graph
    {
        private readonly List<Node> nodes;

        public Graph()
        {
            this.nodes = new List<Node>();
        }

        public IEnumerable<Node> RootNodes { get { return nodes.Where(n => n.IsRootNode()); } }
        public IEnumerable<Node> LeafNodes { get { return nodes.Where(n => n.IsLeafNode()); } }

        public IList<Node> Nodes { get { return nodes; } }
        
        public IEnumerable<Node> FindLongestPath()
        {
            return FindLongest(new List<Node>(), RootNodes);
        }

        private List<Node> FindLongest(List<Node> pathSoFar, IEnumerable<Node> nodesToTest)
        {
            var longest = pathSoFar;
            foreach (var node in nodesToTest)
            {
                var newPath = new List<Node>();
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
