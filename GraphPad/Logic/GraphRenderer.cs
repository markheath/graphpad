using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using Petzold.Media2D;

namespace GraphPad.Logic
{
    class GraphRenderer
    {
        private Canvas canvas;
        private Dictionary<string, Node> nodes;
        private const double nodePadding = 10.0;

        public GraphRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.nodes = new Dictionary<string, Node>();
        }

        public void Render(Graph graph)
        {
            canvas.Children.Clear();
            this.nodes.Clear();
            double top = nodePadding;
            double left = nodePadding;
            // create nodes
            foreach (var nodeInfo in graph.Nodes)
            {
                var node = CreateNode(left, top, nodeInfo.Name);
                left += node.Width + 10;
                canvas.Children.Add(node);
                nodes[nodeInfo.Name] = node;
            }
            bool overlaps = false;
            do
            {
                // now reposition nodes that are overlapped down
                // nodes are in x order
                for (int nodeIndex = 0; nodeIndex < graph.Nodes.Count; nodeIndex++)
                {
                    var nodeInfo = graph.Nodes[nodeIndex];
                    foreach (var connectionInfo in nodeInfo.Children)
                    {
                        var connectedNodeIndex = graph.Nodes.IndexOf(connectionInfo);
                        if (Math.Abs(connectedNodeIndex - nodeIndex) > 1)
                        {
                            // non-adjacent
                            for (int nodeToMoveIndex = Math.Min(connectedNodeIndex, nodeIndex) + 1; nodeToMoveIndex < Math.Max(connectedNodeIndex, nodeIndex); nodeToMoveIndex++)
                            {
                                MoveDown(graph.Nodes[nodeToMoveIndex]);
                            }
                            
                        }
                    }
                }
            } while (overlaps);

            CreateConnections(graph);

            canvas.Width = nodes.Values.Max(x => (double)x.GetValue(Canvas.LeftProperty) + x.Width + nodePadding);
            canvas.Height = nodes.Values.Max(x => (double)x.GetValue(Canvas.TopProperty) + x.Height + nodePadding);
        }

        private void MoveDown(NodeInfo nodeInfo)
        {
            var node = nodes[nodeInfo.Name];
            double yPosition = (double)node.GetValue(Canvas.TopProperty);

            node.SetValue(Canvas.TopProperty, yPosition + node.Height + nodePadding);
        }

        private void CreateConnections(Graph graph)
        {
            foreach (var nodeInfo in graph.Nodes)
            {
                var node = nodes[nodeInfo.Name];
                foreach (var connectionInfo in nodeInfo.Children)
                {
                    var connection = nodes[connectionInfo.Name];
                    UIElement connector;
                    var midNode = GetNodeMidpoint(node);
                    var midConnection = GetNodeMidpoint(connection);
                    var gridSpacing = node.Width + nodePadding;

                    if (midNode.Y == midConnection.Y)
                    {
                        connector = GetStraightLine(midNode, midConnection, node.Width / 2, Brushes.Black);
                    }
                    else if ((Math.Abs(midNode.X - midConnection.X) <= gridSpacing) &&
                        (Math.Abs(midNode.Y - midConnection.Y) <= gridSpacing))
                    {
                        connector = GetStraightLine(midNode, midConnection, node.Width / 2, Brushes.Green);
                    }
                    else
                    {
                        connector = GetStraightLine(midNode, midConnection, node.Width / 2, Brushes.Red);
                    }
                    canvas.Children.Add(connector);
                }
            }
        }

        private static ArrowLine GetStraightLine(Point from, Point to, double radius, Brush stroke)
        {
            var line = new ArrowLine();
            line.Stroke = stroke;
            line.StrokeThickness = 2.0;
            line.ArrowEnds = ArrowEnds.Start;
            line.ArrowLength = 8;

            // trim the line
            var angle = Math.Atan((to.Y - from.Y) / (to.X - from.X));
            var xOffset = radius * Math.Cos(angle);
            var yOffset = radius * Math.Sin(angle);
            from.X += xOffset;
            from.Y += yOffset;

            to.X -= xOffset;
            to.Y -= yOffset;

            line.X1 = from.X;
            line.Y1 = from.Y;
            line.X2 = to.X;
            line.Y2 = to.Y;
            return line;
        }

        private static Point GetNodeMidpoint(Node node)
        {
            var radius = node.Width / 2;
            return new Point((double)node.GetValue(Canvas.LeftProperty) + radius, (double)node.GetValue(Canvas.TopProperty) + radius);
        }

        private static Node CreateNode(double left, double top, string name)
        {
            var node = new Node();
            node.SetValue(Canvas.LeftProperty, left);
            node.SetValue(Canvas.TopProperty, top);
            node.NodeName = name;
            return node;
        }
    }
}
