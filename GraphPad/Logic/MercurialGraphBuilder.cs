using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mercurial;

namespace GraphPad.Logic
{
    class MercurialGraphBuilder
    {
        public Graph LoadGraph(string mercurialRepoPath)
        {
            var repo = new Repository(mercurialRepoPath);
            var graph = new Graph();

            // create the nodes
            foreach (var commit in repo.Log())
            {
                var node = new Node();
                node.Name = commit.RevisionNumber.ToString();
                node.MetaData["Author"] = commit.AuthorName;
                node.MetaData["Email"] = commit.AuthorEmailAddress;
                node.MetaData["Timestamp"] = commit.Timestamp;
                node.MetaData["Message"] = commit.CommitMessage;
                graph.Nodes.Add(node);
            }

            // add the relationships
            foreach (var commit in repo.Log())
            {
                var node = graph.Nodes.Where(n => n.Name == commit.RevisionNumber.ToString()).First();
                if (commit.LeftParentRevision != -1)
                {
                    var parentNode = graph.Nodes.Where(n => n.Name == commit.LeftParentRevision.ToString()).First();
                    parentNode.AddChild(node);
                }
                // bug in the hg lib? - for some reason, is often 0
                if (commit.RightParentRevision > 0)
                {
                    var parentNode = graph.Nodes.Where(n => n.Name == commit.RightParentRevision.ToString()).First();
                    parentNode.AddChild(node);
                }
            }

            // put in the branch, bookmark, tag pointers

            return graph;
        }
    }
}
