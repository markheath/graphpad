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
using GraphPad.Logic;

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
            GraphBuilder builder = new GraphBuilder();
            GraphRenderer renderer = new GraphRenderer(graphCanvas);
            Graph graph = builder.GenerateGraph(graphText.Text);
            renderer.Render(graph);
        }
    }
}
