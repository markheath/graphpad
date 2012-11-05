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
                RedrawGraph(graph, CreateCommitNodeControl, new Size(200, 80));
            }
        }

        void buttonGit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "Select git repository";
            var result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var gitBuilder = new GitGraphBuilder();
                var graph = gitBuilder.LoadGraph(dialog.SelectedPath);
                RedrawGraph(graph, CreateCommitNodeControl, new Size(200, 80));
            }
        }

        void graphText_TextChanged(object sender, TextChangedEventArgs e)
        {
            RecreateGraph();
        }

        private void RecreateGraph()
        {
            var builder = new GraphBuilder();
            var graph = builder.GenerateGraph(graphText.Text);
            RedrawGraph(graph, CreateCircleNodeControl, new Size(40, 40));
        }

        private void RedrawGraph(Graph graph, Func<Node, Size, UserControl> controlBuilder, Size nodeSize)
        {
            var renderer = new GraphRenderer(graphCanvas, controlBuilder, nodeSize);
            var layout = new GraphLayoutEngine();
            layout.Layout(graph);
            renderer.Render(graph);
        }

        private static UserControl CreateCircleNodeControl(Node node, Size size)
        {
            var nodeControl = new CircularNodeControl();
            nodeControl.NodeName = node.Name;
            return nodeControl;
        }

        private static UserControl CreateCommitNodeControl(Node node, Size size)
        {
            var nodeControl = new CommitNodeControl();
            nodeControl.CommitId = node.Name;
            nodeControl.UserName = (string)node.MetaData["Author"];
            nodeControl.MaxWidth = size.Width;
            nodeControl.MaxHeight = size.Height;
            return nodeControl;
        }
    }
}
