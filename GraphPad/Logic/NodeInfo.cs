using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    class NodeInfo
    {
        public NodeInfo()
        {
            this.Connections = new List<NodeInfo>();
        }
        public string Name { get; set; }
        public IList<NodeInfo> Connections { get; private set; }
    }
}
