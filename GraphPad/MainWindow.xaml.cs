using System;
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
            graphText.TextChanged += graphText_TextChanged;
            buttonGit.Click += buttonGit_Click;
            buttonMercurial.Click += buttonMercurial_Click;
        }

        void buttonMercurial_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Select Mercurial repository";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var builder = new MercurialGraphBuilder();
                var graph = builder.LoadGraph(dialog.SelectedPath);
                RedrawGraph(graph);
            }
        }

        void buttonGit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Select git repository";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                GitGraphBuilder gitBuilder = new GitGraphBuilder();
                var graph = gitBuilder.LoadGraph(dialog.SelectedPath);
                RedrawGraph(graph);
            }
        }

        void graphText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecreateGraph();
        }

        private void RecreateGraph()
        {
            GraphBuilder builder = new GraphBuilder();
            Graph graph = builder.GenerateGraph(graphText.Text);
            RedrawGraph(graph);
        }

        private void RedrawGraph(Graph graph)
        {
            GraphRenderer renderer = new GraphRenderer(graphCanvas, CreateCircleNodeControl, 40, 40);
            GraphLayoutEngine layout = new GraphLayoutEngine();
            layout.Layout(graph);
            renderer.Render(graph);
        }

        private static UserControl CreateCircleNodeControl(Node node)
        {
            var nodeControl = new CircularNodeControl();
            nodeControl.NodeName = node.Name;
            return nodeControl;
        }
    }
}
