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
            foreach (var parentNode in graph.Nodes)
            {
                var parentNodeControl = (INodeControl)parentNode.MetaData["Control"];
                foreach (var childNode in parentNode.Children)
                {
                    var childNodeControl = (INodeControl)childNode.MetaData["Control"];
                    UIElement connector;

                    // the line goes from child to parent in a DAG
                    var from = childNodeControl.GetConnectionPoint(childNode.GetDirectionTo(parentNode));
                    var to = parentNodeControl.GetConnectionPoint(parentNode.GetDirectionTo(childNode));
                    connector = GetStraightLine(from, to, Brushes.Green);
                    //connector = GetPolyLine(from, to, Brushes.Green);
                    canvas.Children.Add(connector);
                }
            }
        }

        private static UIElement GetPolyLine(Point from, Point to, Brush stroke)
        {
            var line = new ArrowPolyline();
            line.Stroke = stroke;
            line.StrokeThickness = 2.0;
            line.ArrowEnds = ArrowEnds.End;
            line.ArrowLength = 8;

            line.Points.Add(from);

            if (from.Y != to.Y)
            {
                line.Points.Add(new Point(from.X, to.Y));
            }
            line.Points.Add(to);
            return line;
        }

        private static ArrowLine GetStraightLine(Point from, Point to, Brush stroke)
        {
            var line = new ArrowLine();
            line.Stroke = stroke;
            line.StrokeThickness = 2.0;
            line.ArrowEnds = ArrowEnds.End;
            line.ArrowLength = 8;

            line.X1 = from.X;
            line.Y1 = from.Y;
            line.X2 = to.X;
            line.Y2 = to.Y;
            return line;
        }

        private static UserControl CreateNodeControl(double left, double top, string name)
        {
            var nodeControl = new CircularNodeControl();
            nodeControl.SetValue(Canvas.LeftProperty, left);
            nodeControl.SetValue(Canvas.TopProperty, top);
            nodeControl.NodeName = name;
            return nodeControl;
        }
    }
}
