using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GraphPad.Tests
{
    [TestFixture]
    public class ConnectionDirectionUtilsTests
    {
        [TestCase(0, 0, 0, 1, ConnectionDirection.East)]
        [TestCase(0, 1, 0, 0, ConnectionDirection.West)]
        [TestCase(1, 0, 0, 0, ConnectionDirection.North)]
        [TestCase(0, 0, 1, 0, ConnectionDirection.South)]
        [TestCase(0, 0, 1, 1, ConnectionDirection.SouthEast)]
        [TestCase(1, 0, 0, 1, ConnectionDirection.NorthEast)]
        [TestCase(0, 1, 1, 0, ConnectionDirection.SouthWest)]
        [TestCase(1, 1, 0, 0, ConnectionDirection.NorthWest)]
        public void CorrectConnectionDirection(int fromRow, int fromCol, int toRow, int toCol, ConnectionDirection direction)
        {
            var actualDirection = ConnectionDirectionUtils.GetDirection(fromRow, fromCol, toRow, toCol);
            Assert.AreEqual(direction, actualDirection);
        }
    }
}
