using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GraphPad.Logic
{
    class GraphLayoutEngine
    {
        private Grid grid;
        private int maxRows;

        public GraphLayoutEngine()
        {

        }

        /// <summary>
        /// Will put metadata against the graph's nodes that a renderer can use
        /// </summary>
        /// <param name="graph"></param>
        public void Layout(Graph graph)
        {
            graph.Sort();

            int column = 0;
            this.maxRows = graph.Nodes.Count;
            this.grid = new Grid(maxRows);

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
                    grid[n.GetRow(), n.GetColumn()] = true;
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
                        grid[c.GetRow(), c.GetColumn()] = true;
                    }
                }
            }

            // optional stage
            ShuffleLeft(graph);
        }

        private void ShuffleLeft(Graph graph)
        {
            foreach (Node n in graph.Nodes)
            {
                int row = n.GetRow();
                int col = n.GetColumn();
                // only bother if we could move left
                if (col > 0 && grid[row, col - 1] == false)
                {
                    // 1. work out min column
                    int minColumn = n.Parents.Count() > 0 ? n.Parents.Max(p => p.GetColumn()) + 1 : 0;
                    // 2. see if any space is free to left
                    // not sure whether to go forward or back here
                    for (int newCol = minColumn; newCol < col; newCol++)
                    {
                        if (grid[row, newCol] == false)
                        {
                            grid[row, col] = false; // free up the old pos
                            n.SetColumn(newCol);
                            grid[row, newCol] = true; // free up the old pos
                            break;
                        }
                    }
                }
            }
        }

        private int GetFreeRow(int column, int startRow)
        {
            for (int row = startRow; row < maxRows; row++)
            {
                if (grid[row, column] == false)
                {
                    return row;
                }
            }
            throw new InvalidOperationException("Failed to find a free row");
        }
    }
}
