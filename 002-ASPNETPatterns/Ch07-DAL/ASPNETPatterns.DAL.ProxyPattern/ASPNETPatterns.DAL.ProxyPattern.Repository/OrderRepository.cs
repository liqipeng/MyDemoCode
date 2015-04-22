using ASPNETPatterns.DAL.ProxyPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.ProxyPattern.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> FindAllBy(Guid customerId)
        {
            return new List<Order>() 
            {
                new Order(){ Id = Guid.NewGuid(), OrderDate = DateTime.Now },
                new Order(){ Id = Guid.NewGuid(), OrderDate = DateTime.Now.AddDays(-1) },
                new Order(){ Id = Guid.NewGuid(), OrderDate = DateTime.Now.AddDays(-2).AddHours(2) },
                new Order(){ Id = Guid.NewGuid(), OrderDate = DateTime.Now.AddDays(-3).AddMinutes(53) }
            };
        }
    }
}
