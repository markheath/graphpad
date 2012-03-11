using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibGit2Sharp;

namespace GraphPad.Logic
{
    class GitGraphBuilder
    {
        public Graph LoadGraph(string gitRepoPath)
        {
            var repo = new Repository(gitRepoPath);
            var graph = new Graph();

            // create the nodes
            foreach (var commit in repo.Commits)
            {
                var node = new Node();
                node.Name = commit.Sha;
                node.MetaData["Author"] = commit.Author.Name;
                node.MetaData["Email"] = commit.Author.Email;
                node.MetaData["Timestamp"] = commit.Author.When;
                node.MetaData["Message"] = commit.Message;
                graph.Nodes.Add(node);
            }

            // add the relationships
            foreach (var commit in repo.Commits)
            {
                var node = graph.Nodes.Where(n => n.Name == commit.Sha).First();
                foreach (var parent in commit.Parents)
                {
                    var parentNode = graph.Nodes.Where(n => n.Name == parent.Sha).First();
                    parentNode.AddChild(node);
                }
            }

            // put in the branch pointers

            return graph;
        }
    }
}
