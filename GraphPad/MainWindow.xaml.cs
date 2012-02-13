﻿using System;
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
                    Ellipse node = CreateNode(left, top);
                    left += node.Width + 10;
                    graphCanvas.Children.Add(node);
                }
            }
        }

        private static Ellipse CreateNode(double left, double top)
        {
            Ellipse node = new Ellipse();
            node.Width = 40;
            node.Height = 40;
            node.SetValue(Canvas.LeftProperty, left);
            node.SetValue(Canvas.TopProperty, top);
            node.Stroke = Brushes.Black;
            node.StrokeThickness = 2;
            return node;
        }
    }
}