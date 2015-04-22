using ASPNETPatterns.DAL.ProxyPattern.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.ProxyPattern.Repository
{
    public class CustomerProxy:Customer
    {
        private bool _haveOrdersLoaded = false;
        private IEnumerable<Order> _orders;

        public IOrderRepository OrderRepository { get; set; }

        public bool HaveLoadedOrders() 
        {
            return _haveOrdersLoaded;
        }

        public override IEnumerable<Order> Orders
        {
            get
            {
                if (!HaveLoadedOrders())
                {
                    RetrieveOrders();
                    _haveOrdersLoaded = true;
                }

                return _orders;
            }
            set
            {
                base.Orders = value;
            }
        }

        private void RetrieveOrders()
        {
            _orders = OrderRepository.FindAllBy(base.Id);
        }
    }
}
