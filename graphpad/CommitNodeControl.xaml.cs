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
    /// Interaction logic for CommitNodeControl.xaml
    /// </summary>
    public partial class CommitNodeControl : UserControl, INodeControl
    {
        public CommitNodeControl()
        {
            InitializeComponent();
        }

        public string CommitId
        {
            get { return (string)GetValue(CommitIdProperty); }
            set { SetValue(CommitIdProperty, value); }
        }

        public static readonly DependencyProperty CommitIdProperty =
            DependencyProperty.Register("CommitId", typeof(string), typeof(CommitNodeControl), new UIPropertyMetadata("f178a4234"));

        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty =
            DependencyProperty.Register("UserName", typeof(string), typeof(CommitNodeControl), new UIPropertyMetadata("Mark Heath"));


        public Point GetConnectionPoint(ConnectionDirection connectionDirection)
        {
            var fromPosition = new Point((double)this.GetValue(Canvas.LeftProperty), (double)this.GetValue(Canvas.TopProperty));
            var width = this.ActualWidth;
            var height = this.ActualHeight;
            var corner = width/4;

            switch (connectionDirection)
            {
                case ConnectionDirection.West:
                    return new Point(fromPosition.X, fromPosition.Y + height/2);
                case ConnectionDirection.East:
                    return new Point(fromPosition.X + width, fromPosition.Y + height/2);
                case ConnectionDirection.North:
                    return new Point(fromPosition.X + width/2, fromPosition.Y);
                case ConnectionDirection.South:
                    return new Point(fromPosition.X + width/2, fromPosition.Y + height);
                case ConnectionDirection.NorthWest:
                    return new Point(fromPosition.X + corner, fromPosition.Y);
                case ConnectionDirection.SouthWest:
                    return new Point(fromPosition.X + corner, fromPosition.Y + height);
                case ConnectionDirection.NorthEast:
                    return new Point(fromPosition.X + width - corner, fromPosition.Y);
                case ConnectionDirection.SouthEast:
                    return new Point(fromPosition.X + width - corner, fromPosition.Y + height);
                default:
                    throw new ArgumentException("Invalid connection direction");
            }
        }
    }
}
