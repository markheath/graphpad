using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GraphPad.Logic
{
    class GraphLayoutEngine
    {
        public GraphLayoutEngine()
        {

        }

        /// <summary>
        /// Will put metadata against the graph's nodes that a renderer can use
        /// </summary>
        /// <param name="graph"></param>
        public void Layout(Graph graph)
        {
            int column = 0;
            this.graph = graph;
            grid = new BitArray(graph.Nodes.Count * graph.Nodes.Count);

            // put one node into each column
            foreach (Node n in graph.Nodes)
            {
                n.SetColumn(column++);
            }

            foreach (Node n in graph.Nodes)
            {
                if (!n.HasRow())
                {
                    // root nodes only
                    n.SetRow(GetFreeRow(n.GetColumn(), 0));
                }

                int startingRow = n.GetRow();
                foreach (Node c in n.Children)
                {
                    if (c.HasRow())
                    {
                        // already laid out
                    }
                    else
                    {
                        c.SetRow(GetFreeRow(c.GetColumn(), startingRow));
                        startingRow = c.GetRow() + 1;
                    }
                }
            }
        }

        private BitArray grid;
        private Graph graph;

        private int GetFreeRow(int column, int startRow)
        {
            for (int row = startRow; row < graph.Nodes.Count; row++)
            {
                if (!grid[(column * graph.Nodes.Count) + row])
                {
                    return row;
                }
            }
            throw new InvalidOperationException("Failed to find a free row");
        }
    }
}
