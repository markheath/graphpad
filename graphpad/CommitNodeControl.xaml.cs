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
    public partial class CommitNodeControl : UserControl
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



    }
}
