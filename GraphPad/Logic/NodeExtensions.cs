using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    public static class NodeExtensions
    {
        public static bool HasRow(this Node node)
        {
            return node.MetaData.ContainsKey("Row");
        }

        public static int GetRow(this Node node)
        {
            return (int)node.MetaData["Row"];
        }

        public static void SetRow(this Node node, int row)
        {
            node.MetaData["Row"] = row;
        }

        public static int GetColumn(this Node node)
        {
            return (int)node.MetaData["Column"];
        }

        public static void SetColumn(this Node node, int column)
        {
            node.MetaData["Column"] = column;
        }

    }
}
