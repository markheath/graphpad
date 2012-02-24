using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class GraphTests
    {
        [Test]
        public void CanFindRoot()
        {
            NodeInfo root = new NodeInfo() { Name = "root" };
            NodeInfo a = new NodeInfo() { Name = "a" };
            root.AddChild(a);
            Graph g = new Graph();
            g.Nodes.Add(root);
            g.Nodes.Add(a);

            Assert.AreEqual(1, g.RootNodes.Count());
            Assert.AreEqual(root, g.RootNodes.First());

        }

        [Test]
        public void CanFindLeaf()
        {
            NodeInfo root = new NodeInfo() { Name = "root" };
            NodeInfo middle = new NodeInfo() { Name = "middle" };
            NodeInfo leaf = new NodeInfo() { Name = "leaf" };
            root.AddChild(middle);
            middle.AddChild(leaf);
            Graph g = new Graph();
            g.Nodes.Add(root);
            g.Nodes.Add(middle);
            g.Nodes.Add(leaf);

            Assert.AreEqual(1, g.LeafNodes.Count());
            Assert.AreEqual(leaf, g.LeafNodes.First());
        }

        [Test]
        public void CanFindLongestPathSimple()
        {
            Graph g = CreateGraph(3);
            var longest = g.FindLongestPath();
            Assert.AreEqual(3, longest.Count());
        }

        [Test]
        public void CanFindLongestPathComplex()
        {
            Graph g = CreateGraph(3);
            g.Nodes.Add(new NodeInfo() { Name = "3" });
            g.Nodes.Add(new NodeInfo() { Name = "4" });
            g.Nodes.Add(new NodeInfo() { Name = "5" });
            g.Nodes[1].AddChild(g.Nodes[3]);
            g.Nodes[3].AddChild(g.Nodes[4]);
            g.Nodes[4].AddChild(g.Nodes[5]);

            var longest = g.FindLongestPath();
            Assert.AreEqual(5, longest.Count());
        }

        private Graph CreateGraph(int nodes)
        {
            Graph g = new Graph();
            for (int n = 0; n < nodes; n++)
            {
                g.Nodes.Add(new NodeInfo() { Name = n.ToString() });
                if (n > 0)
                {
                    g.Nodes[n-1].AddChild(g.Nodes[n]);
                }
            }
            return g;
        }
    }
}
