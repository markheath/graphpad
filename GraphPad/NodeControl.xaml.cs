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
using System.ComponentModel;

namespace GraphPad
{
    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class NodeControl : UserControl
    {
        private List<NodeControl> connections = new List<NodeControl>();
        
        public NodeControl()
        {
            InitializeComponent();
        }

        public List<NodeControl> Connections
        {
            get { return connections; }
        }

        public string NodeName
        {
            get { return (string)GetValue(NodeNameProperty); }
            set { SetValue(NodeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NodeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NodeNameProperty =
            DependencyProperty.Register("NodeName", typeof(string), typeof(NodeControl), new UIPropertyMetadata("A", new PropertyChangedCallback(OnNodeNameChanged)));

        private static void OnNodeNameChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var node = (NodeControl)sender;
            node.nodeNameTextBlock.Text = (string)args.NewValue;
        }

    }
}
