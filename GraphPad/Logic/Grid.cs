using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GraphPad.Logic
{
    class Grid
    {
        private BitArray grid;
        private int maxRows;

        public Grid(int maxRows)
        {
            this.maxRows = maxRows;
            this.grid = new BitArray(this.maxRows * this.maxRows);
        }

        public bool this[int row, int column]
        {
            get
            {
                return this.grid[(column * maxRows) + row];
            }
            set
            {
                this.grid[(column * maxRows) + row] = value;
            }
        }
    }
}
