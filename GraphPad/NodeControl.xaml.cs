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
using System.ComponentModel;

namespace GraphPad
{
    public interface INodeControl
    {
        Point GetConnectionPoint(ConnectionDirection connectionDirection);
    }

    /// <summary>
    /// Interaction logic for Node.xaml
    /// </summary>
    public partial class NodeControl : UserControl, INodeControl
    {
        public NodeControl()
        {
            InitializeComponent();
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

        public Point GetConnectionPoint(ConnectionDirection connectionDirection)
        {
            var from = this;
            var fromPosition = new Point((double)from.GetValue(Canvas.LeftProperty), (double)from.GetValue(Canvas.TopProperty));
            
            var radius = this.Width/2;
            var corner = radius - Math.Sqrt((radius*radius)/2);

            switch (connectionDirection)
            {
                case ConnectionDirection.West:
                    return new Point(fromPosition.X, fromPosition.Y + from.Height / 2);
                case ConnectionDirection.East:
                    return new Point(fromPosition.X + from.Width, fromPosition.Y + from.Height/2);
                case ConnectionDirection.North:
                    return new Point(fromPosition.X + from.Width / 2, fromPosition.Y);
                case ConnectionDirection.South:
                    return new Point(fromPosition.X + from.Width / 2, fromPosition.Y + from.Height);
                case ConnectionDirection.NorthWest:
                    return new Point(fromPosition.X + corner, fromPosition.Y + corner);
                case ConnectionDirection.SouthWest:
                    return new Point(fromPosition.X + corner, fromPosition.Y + from.Height - corner);
                case ConnectionDirection.NorthEast:
                    return new Point(fromPosition.X + from.Width - corner, fromPosition.Y + corner);
                case ConnectionDirection.SouthEast:
                    return new Point(fromPosition.X + from.Width - corner, fromPosition.Y + from.Height - corner);
                default:
                    throw new ArgumentException("Invalid connection direction");
            }
        }
    }
}
