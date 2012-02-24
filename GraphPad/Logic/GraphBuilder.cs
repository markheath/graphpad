using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class GraphBuilder
    {
        public Graph GenerateGraph(string text)
        {
            var tokens = Tokenizer.Tokenize(text);
            var nodes = new Dictionary<string, NodeInfo>();
            NodeInfo lastNode = null;
            foreach (var token in tokens)
            {
                switch (token)
                {
                    case ">":
                    case "<":
                    case "-":
                        break;
                    case "":
                        // ignore
                        break;
                    case "%NEWLINE%":
                        // started a new line
                        lastNode = null;
                        break;
                    default:
                        NodeInfo node;
                        if (nodes.ContainsKey(token))
                        {
                            node = nodes[token];
                        }
                        else
                        {
                            node = new NodeInfo() { Name = token };
                            nodes[token] = node;
                        }
                        if (lastNode != null)
                        {
                            lastNode.AddChild(node);
                        }
                        lastNode = node;
                        break;
                }
            }
            var g = new Graph();
            foreach (var n in nodes.Values)
                g.Nodes.Add(n);
            return g;
        }

    }
}
