using ASPNETPatterns.DAL.ProxyPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.ProxyPattern.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
        private IOrderRepository _orderRepository;

        public CustomerRepository(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Customer FindBy(Guid id)
        {
            Customer customer = new CustomerProxy();
            customer.Id = Guid.NewGuid();
            customer.Name = "Tom";

            ((CustomerProxy)customer).OrderRepository = _orderRepository;

            return customer;
        }
    }
}
