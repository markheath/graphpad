using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class Connection
    {
        public Node TargetNode { get; set; }
        public RelationshipType Relationship { get; set; }
    }
}
