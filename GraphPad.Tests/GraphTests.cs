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
    }
}
