using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowDepth
{
    public class DepthData
    {
        public decimal[][] asks { get; set; }
        public decimal[][] bids { get; set; }
    }

    public class Data 
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }
}
