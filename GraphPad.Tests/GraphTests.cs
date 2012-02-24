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
        private GraphBuilder builder = new GraphBuilder();
        [Test]
        public void CanFindRoot()
        {
            Graph g = builder.GenerateGraph("root-a");
            Assert.AreEqual(1, g.RootNodes.Count());
            Assert.AreEqual("root", g.RootNodes.First().Name);
        }

        [Test]
        public void CanFindLeaf()
        {
            Graph g = builder.GenerateGraph("root-middle-leaf");

            Assert.AreEqual(1, g.LeafNodes.Count());
            Assert.AreEqual("leaf", g.LeafNodes.First().Name);
        }

        [Test]
        public void CanFindLongestPathSimple()
        {
            Graph g = builder.GenerateGraph("a-b-c");
            var longest = g.FindLongestPath();
            Assert.AreEqual(3, longest.Count());
        }

        [Test]
        public void CanFindLongestPathComplex()
        {
            Graph g = builder.GenerateGraph("a-b-c\nb-d-e-f");

            var longest = g.FindLongestPath();
            Assert.AreEqual(5, longest.Count());
        }
    }
}
