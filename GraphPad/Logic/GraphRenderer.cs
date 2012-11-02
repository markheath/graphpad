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
        private const double nodePadding = 10.0;
        private const double nodeWidth = 40.0;
        private const double nodeHeight = 40.0;

        public GraphRenderer(Canvas canvas)
        {
            this.canvas = canvas;
        }

        public void Render(Graph graph)
        {
            canvas.Children.Clear();

            // create nodes
            foreach (var node in graph.Nodes)
            {
                var left = node.GetColumn() * (nodePadding + nodeWidth);
                var top = node.GetRow() * (nodePadding + nodeHeight);
                var nodeControl = CreateNodeControl(left, top, node.Name);
                canvas.Children.Add(nodeControl);
                node.MetaData["Control"] = nodeControl;
            }
            CreateConnections(graph);

            canvas.Width = canvas.Children.OfType<UserControl>().Max(x => (double)x.GetValue(Canvas.LeftProperty) + x.Width + nodePadding);
            canvas.Height = canvas.Children.OfType<UserControl>().Max(x => (double)x.GetValue(Canvas.TopProperty) + x.Height + nodePadding);
        }

        private void CreateConnections(Graph graph)
        {
            foreach (var node in graph.Nodes)
            {
                var nodeControl = (UserControl)node.MetaData["Control"];
                foreach (var connection in node.Children)
                {
                    var connectedNodeControl = (UserControl)connection.MetaData["Control"];
                    UIElement connector;
                    var midNode = GetNodeMidpoint(nodeControl);
                    var midConnection = GetNodeMidpoint(connectedNodeControl);
                    var gridSpacing = nodeControl.Width + nodePadding;

                    if (node.GetRow() == connection.GetRow())
                    {
                        connector = GetStraightLine(midNode, midConnection, nodeControl.Width / 2, Brushes.Black);
                    }
                    else if ((Math.Abs(node.GetRow() - connection.GetRow()) <= 1) &&
                        (Math.Abs(node.GetColumn() - node.GetColumn()) <= 1))
                    {
                        connector = GetStraightLine(midNode, midConnection, nodeControl.Width / 2, Brushes.Green);
                    }
                    else
                    {
                        var from = GetConnectionPoint(nodeControl, connectedNodeControl);
                        var to = GetConnectionPoint(connectedNodeControl, nodeControl);
                        connector = GetPolyLine(from, to, Brushes.Red);
                    }
                    canvas.Children.Add(connector);
                }
            }
        }

        private Point GetConnectionPoint(UserControl from, UserControl to)
        {
            var connectionPoint = new Point();
            var fromPosition = new Point((double)from.GetValue(Canvas.LeftProperty), (double)from.GetValue(Canvas.TopProperty));
            var toPosition = new Point((double)to.GetValue(Canvas.LeftProperty), (double)to.GetValue(Canvas.TopProperty));

            if (fromPosition.Y < toPosition.Y)
            {
                // bottom
                connectionPoint.Y = fromPosition.Y + from.Height;
                connectionPoint.X = fromPosition.X + from.Width / 2;
            }
            else if (fromPosition.Y == toPosition.Y)
            {
                connectionPoint.Y = fromPosition.Y + from.Height / 2;
                if (fromPosition.X < toPosition.X)
                {
                    // right edge
                    connectionPoint.X = fromPosition.X + from.Width;
                }
                else
                {
                    connectionPoint.X = fromPosition.X;
                }
            }
            else
            {
                connectionPoint.Y = fromPosition.Y + from.Height / 2;
                if (toPosition.X > fromPosition.X)
                    connectionPoint.X = fromPosition.X + from.Width;
                else
                    connectionPoint.X = fromPosition.X;
                
            }
            return connectionPoint;
        }

        private static UIElement GetPolyLine(Point from, Point to, Brush stroke)
        {
            var line = new ArrowPolyline();
            line.Stroke = stroke;
            line.StrokeThickness = 2.0;
            line.ArrowEnds = ArrowEnds.Start;
            line.ArrowLength = 8;

            line.Points.Add(from);

            if (from.Y != to.Y)
            {
                line.Points.Add(new Point(from.X, to.Y));
            }
            line.Points.Add(to);
            return line;
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

        private static Point GetNodeMidpoint(UserControl node)
        {
            var radius = node.Width / 2;
            return new Point((double)node.GetValue(Canvas.LeftProperty) + radius, (double)node.GetValue(Canvas.TopProperty) + radius);
        }

        private static UserControl CreateNodeControl(double left, double top, string name)
        {
            var nodeControl = new NodeControl();
            nodeControl.SetValue(Canvas.LeftProperty, left);
            nodeControl.SetValue(Canvas.TopProperty, top);
            nodeControl.NodeName = name;
            return nodeControl;
        }
    }
}
