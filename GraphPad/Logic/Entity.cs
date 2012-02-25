using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphPad.Logic
{
    /// <summary>
    /// base class for anything that needs to store additional metadata
    /// </summary>
    public class Entity
    {
        private readonly Dictionary<string,object> metaData;

        public Entity()
        {
            this.metaData = new Dictionary<string, object>();
        }

        public IDictionary<string, object> MetaData { get { return this.metaData; } }
    }
}
