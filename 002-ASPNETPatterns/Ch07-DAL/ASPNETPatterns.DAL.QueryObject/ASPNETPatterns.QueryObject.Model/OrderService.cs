using ASPNETPatterns.QueryObject.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Model
{
    public class OrderService
    {
        const string fieldName_CustomerId = "CustomerId";
        const string fieldName_OrderDate = "OrderDate";

        private IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public IEnumerable<Order> FindAllCustomersOrderBy(Guid customerId) 
        {
            IEnumerable<Order> cusomterOrders = new List<Order>();

            Query query = new Query();
            query.Add(new Criterion(fieldName_CustomerId, customerId, CriteriaOperator.Equal));
            query.OrderByProperty = new OrderByClause(){ PropertyName = fieldName_CustomerId, Desc = true };

            cusomterOrders = _orderRepository.FindBy(query);

            return cusomterOrders;
        }

        public IEnumerable<Order> FindAllCustomerOrdersWithInOrderDateBy(Guid customerId, DateTime orderDate) 
        {
            IEnumerable<Order> customerOrders = new List<Order>();

            Query query = new Query();
            query.Add(new Criterion(fieldName_CustomerId, orderDate, CriteriaOperator.Equal));
            query.QueryOperator = QueryOperator.And;
            query.Add(new Criterion(fieldName_OrderDate, orderDate, CriteriaOperator.LessThanOrEqual));
            query.OrderByProperty = new OrderByClause() { PropertyName = fieldName_OrderDate, Desc = true };

            customerOrders = _orderRepository.FindBy(query);

            return customerOrders;
        }

        public IEnumerable<Order> FindAllCustomerOrdersUsingAComplexQueryWith(Guid customerId)
        {
            IEnumerable<Order> customerOrders = new List<Order>();

            Query query = NamedQueryFactory.CreateRetrieveOrdersUsingAComplexQuery(customerId);

            customerOrders = _orderRepository.FindBy(query);

            return customerOrders;
        }

    }
}
