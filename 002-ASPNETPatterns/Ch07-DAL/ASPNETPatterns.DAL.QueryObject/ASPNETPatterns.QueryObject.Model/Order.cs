using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Model
{
    public class Order
    {
        public Guid CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid Id { get; set; }
    }
}
