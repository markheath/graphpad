using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class NodeInfoTests
    {
        [Test]
        public void CanAddAParentRelationship()
        {
            var parent = new NodeInfo() { Name = "parent" };
            var child = new NodeInfo() { Name = "child" };
            parent.AddChild(child);
            Assert.AreEqual(1, parent.Children.Count());
            Assert.AreEqual(0, parent.Parents.Count());
            Assert.AreEqual(0, child.Children.Count());
            Assert.AreEqual(1, child.Parents.Count());
        }
    }
}
