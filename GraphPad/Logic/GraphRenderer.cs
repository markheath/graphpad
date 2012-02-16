using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace GraphPad.Logic
{
    class GraphRenderer
    {
        private Canvas canvas;
        private Dictionary<string, Node> nodes;

        public GraphRenderer(Canvas canvas)
        {
            this.canvas = canvas;
            this.nodes = new Dictionary<string, Node>();
        }

        public void Render(Graph graph)
        {
            canvas.Children.Clear();
            this.nodes.Clear();
            double top = 10;
            double left = 10;
            // create nodes
            foreach (var nodeInfo in graph.Nodes)
            {
                var node = CreateNode(left, top, nodeInfo.Name);
                left += node.Width + 10;
                canvas.Children.Add(node);
                nodes[nodeInfo.Name] = node;
            }
            CreateConnections(graph);
        }

        private void CreateConnections(Graph graph)
        {
            foreach (var nodeInfo in graph.Nodes)
            {
                var node = nodes[nodeInfo.Name];
                foreach (var connectionInfo in nodeInfo.Connections)
                {
                    var connection = nodes[connectionInfo.Name];
                    var line = new Line();
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 2.0;
                    var from = GetNodeMidpoint(node);
                    var to = GetNodeMidpoint(connection);

                    // trim the line
                    var angle = Math.Asin((to.Y - from.Y) / (to.X - from.X));
                    var radius = node.Width / 2;
                    from.X += radius * Math.Cos(angle);
                    from.Y += radius * Math.Sin(angle);

                    to.X -= radius * Math.Cos(angle);
                    from.Y -= radius * Math.Sin(angle);

                    line.X1 = from.X;
                    line.Y1 = from.Y;
                    line.X2 = to.X;
                    line.Y2 = to.Y;
                    canvas.Children.Add(line);
                }
            }
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
