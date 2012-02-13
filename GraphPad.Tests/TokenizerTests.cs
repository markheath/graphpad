using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace GraphPad.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void CanParseBasicDirectedEdge()
        {
            string[] tokens = Tokenizer.Tokenize("a>b");
            string[] expected = { "a", ">", "b" };
            Assert.AreEqual(expected, tokens);
        }
    }
}
