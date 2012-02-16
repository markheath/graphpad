using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using GraphPad.Logic;

namespace GraphPad.Tests
{
    [TestFixture]
    public class TokenizerTests
    {
        [Test]
        public void CanParseBasicDirectedEdge()
        {
            var tokens = Tokenizer.Tokenize("a>b");
            string[] expected = { "a", ">", "b" };
            Assert.AreEqual(expected, tokens);
        }
        
        [Test]
        public void IgnoreWhitespace()
        {
            var tokens = Tokenizer.Tokenize("a > b");
            string[] expected = { "a", ">", "b" };
            Assert.AreEqual(expected, tokens);
        }

        [Test]
        public void CanParseUndirectedEdge()
        {
            var tokens = Tokenizer.Tokenize("a-b");
            string[] expected = { "a", "-", "b" };
            Assert.AreEqual(expected, tokens);
        }
    }
}
