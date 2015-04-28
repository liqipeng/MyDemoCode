using ASPNETPatterns.QueryObject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPNETPatterns.QueryObject.Model
{
    public interface IOrderRepository
    {       
         IEnumerable<Order> FindBy(Query query);
    }
}
