using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class GraphBuilderTests
    {
        [Test]
        public void ParsesASingleNode()
        {
            var builder = new GraphBuilder();
            Graph g = builder.GenerateGraph("a");
            Assert.AreEqual(1, g.Nodes.Count);
            Assert.AreEqual("a", g.Nodes[0].Name);
        }

        [Test]
        public void ParsesTwoNodes()
        {
            var builder = new GraphBuilder();
            Graph g = builder.GenerateGraph("a-b");
            Assert.AreEqual(2, g.Nodes.Count);
            Assert.AreEqual("a", g.Nodes[0].Name);
            Assert.AreEqual("b", g.Nodes[1].Name);
        }

        [Test]
        public void ConnectsTwoNodes()
        {
            var builder = new GraphBuilder();
            Graph g = builder.GenerateGraph("a-b");
            Assert.AreEqual(1, g.Nodes[0].Children.Count());
            Assert.AreEqual("b", g.Nodes[0].Children.First().Name);
        }

        [Test]
        public void CanAcceptNewLine()
        {
            var builder = new GraphBuilder();
            Graph g = builder.GenerateGraph("a-b\r\nc");
            Assert.AreEqual(3, g.Nodes.Count);
        }
    }
}
