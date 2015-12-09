using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorPullService
{
    public class VectorDebugLog : FlatFileEndpoint
    {
        public VectorDebugLog() : base("VectorLog")
        {

        }

        //Override the inherited Name
        public override string Name
        {
            get
            {
                return "VectorDebugLog";
            }
        }
    }
}
