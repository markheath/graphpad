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

        private Dictionary<string, FrameworkElement> nodes = new Dictionary<string, FrameworkElement>();

        private void RecreateGraph()
        {
            var tokens = Tokenizer.Tokenize(graphText.Text);
            graphCanvas.Children.Clear();
            nodes.Clear();
            double top = 10;
            double left = 10;
            foreach (var token in tokens)
            {
                if (token == ">")
                {

                }
                else if (token.Trim().Length > 0)
                {
                    var node = CreateNode(left, top, token);
                    left += node.Width + 10;
                    graphCanvas.Children.Add(node);
                }
            }
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
