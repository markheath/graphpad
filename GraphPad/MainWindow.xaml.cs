using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GraphPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.graphText.TextChanged += new TextChangedEventHandler(graphText_TextChanged);
        }

        void graphText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecreateGraph();
        }

        private Dictionary<string, Node> nodes = new Dictionary<string, Node>();

        private void RecreateGraph()
        {
            var tokens = Tokenizer.Tokenize(graphText.Text);
            graphCanvas.Children.Clear();
            nodes.Clear();
            double top = 10;
            double left = 10;
            Node lastNode = null;
            foreach (var token in tokens)
            {
                switch(token)
                {
                    case ">":
                    case "<":
                    case "-":
                        break;
                    default:
                        Node node;
                        if (nodes.ContainsKey(token))
                        {
                            node = nodes[token];
                        }
                        else
                        {
                            node = CreateNode(left, top, token);
                            left += node.Width + 10;
                            graphCanvas.Children.Add(node);
                            nodes[token] = node;
                        }
                        if (lastNode != null)
                        {
                            lastNode.Connections.Add(node);
                        }
                        lastNode = node;
                        break;
                }
            }
            CreateConnections();
        }

        private void CreateConnections()
        {
            foreach (var node in nodes.Values)
            {
                foreach (var connection in node.Connections)
                {
                    var line = new Line();
                    line.Stroke = Brushes.Black;
                    line.StrokeThickness = 2.0;
                    var from = GetNodeMidpoint(node);
                    var to = GetNodeMidpoint(connection);

                    // trim the line
                    var angle = Math.Asin(to.Y - from.Y / to.X - from.X);
                    var radius = node.Width / 2;
                    from.X += radius * Math.Cos(angle);
                    from.Y += radius * Math.Sin(angle);

                    to.X -= radius * Math.Cos(angle);
                    from.Y -= radius * Math.Sin(angle);


                    line.X1 = from.X;
                    line.Y1 = from.Y;
                    line.X2 = to.X;
                    line.Y2 = to.Y;
                    graphCanvas.Children.Add(line);
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
