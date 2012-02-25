using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void CanAddAParentRelationship()
        {
            var parent = new Node() { Name = "parent" };
            var child = new Node() { Name = "child" };
            parent.AddChild(child);
            Assert.AreEqual(1, parent.Children.Count());
            Assert.AreEqual(0, parent.Parents.Count());
            Assert.AreEqual(0, child.Children.Count());
            Assert.AreEqual(1, child.Parents.Count());
        }

        [Test]
        public void CanGetLongestDistanceToAllParents()
        {
            var builder = new GraphBuilder();
            var g = builder.GenerateGraph("a-b-c-d-e-f\na-f");
            var d = g.GetNodeByName("f").FindLongestDistanceToAllAncestors();
            var a = g.GetNodeByName("a");
            var c = g.GetNodeByName("c");
            
            Assert.AreEqual(5, d[a]);
            Assert.AreEqual(3, d[c]);
        }

        [Test]
        public void DenyInvalidDag()
        {
            var builder = new GraphBuilder();
            Assert.Throws<InvalidOperationException>(() => builder.GenerateGraph("a-b-a"));
        }

        [Test]
        public void DenyInvalidComplexCycle()
        {
            var builder = new GraphBuilder();
            Assert.Throws<InvalidOperationException>(() => builder.GenerateGraph("a-b-c\nb-d-e\nd-f-g\ng-b"));
        }
    }
}
