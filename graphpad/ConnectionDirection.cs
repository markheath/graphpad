using GraphPad.Logic;

namespace GraphPad
{
    public enum ConnectionDirection
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    public static class ConnectionDirectionUtils
    {
        public static ConnectionDirection GetDirectionTo(this Node fromNode, Node toNode)
        {
            return GetDirection(fromNode.GetRow(), fromNode.GetColumn(), toNode.GetRow(), toNode.GetColumn());
        }

        public static ConnectionDirection GetDirection(int fromRow, int fromColumn, int toRow, int toColumn)
        {
            if (fromRow == toRow)
            {
                return fromColumn > toColumn ? ConnectionDirection.West : ConnectionDirection.East;
            }
            else if (fromRow > toRow)
            {
                if (fromColumn == toColumn) return ConnectionDirection.North;
                return fromColumn > toColumn ? ConnectionDirection.NorthWest : ConnectionDirection.NorthEast;
            }
            else
            {
                if (fromColumn == toColumn) return ConnectionDirection.South;
                return fromColumn > toColumn ? ConnectionDirection.SouthWest : ConnectionDirection.SouthEast;
            }
        }
    }
}