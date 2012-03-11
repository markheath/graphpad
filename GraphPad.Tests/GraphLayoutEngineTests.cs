using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class GraphLayoutEngineTests
    {
        [Test]
        public void CanLayoutStraightLine()
        {
            GraphLayoutEngine l = new GraphLayoutEngine();
            Graph g = (new GraphBuilder()).GenerateGraph("a-b-c-d");
            l.Layout(g);
            AssertNodeIsOnRow(g, "a", 0);
            AssertNodeIsOnRow(g, "b", 0);
            AssertNodeIsOnRow(g, "c", 0);
            AssertNodeIsOnRow(g, "d", 0);
        }

        [Test]
        public void BranchGoesOnSecondLine()
        {
            GraphLayoutEngine l = new GraphLayoutEngine();
            Graph g = (new GraphBuilder()).GenerateGraph("a-b-c-d\na-e-f-g");
            l.Layout(g);
            AssertNodeIsOnRow(g, "a", 0);
            AssertNodeIsOnRow(g, "b", 0);
            AssertNodeIsOnRow(g, "c", 0);
            AssertNodeIsOnRow(g, "d", 0);
            AssertNodeIsOnRow(g, "e", 1);
            AssertNodeIsOnRow(g, "f", 1);
            AssertNodeIsOnRow(g, "g", 1);
        }

        // failing test, work out how to deal with this
        [Test]
        public void CanLayoutAGraphWithMultipleRootNodes()
        {
            GraphLayoutEngine l = new GraphLayoutEngine();
            Graph g = (new GraphBuilder()).GenerateGraph("a-b-c\na-c\nd-e-f");
            l.Layout(g);
        }

        //  test ideas
        // a-b-c-d\na-e-f-g\nb-h-i-j (should push first branch down?)


        private static void AssertNodeIsOnRow(Graph g, string node, int row)
        {
            Assert.AreEqual(row, g.GetNodeByName(node).GetRow(), node);
        }
    }
}
