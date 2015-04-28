using ASPNETPatterns.QueryObject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.QueryObject.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Order> FindBy(Infrastructure.Query query)
        {
            IList<Order> orders = new List<Order>();

            using (SqlConnection conn = new SqlConnection(_connectionString)) 
            {
                SqlCommand cmd = conn.CreateCommand();
                query.TranslateInfo(cmd);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        orders.Add(new Order() 
                        {
                            CustomerId = new Guid(reader["CustomerId"].ToString()),
                            OrderDate = DateTime.Parse(reader["OrderDate"].ToString()),
                            Id = new Guid(reader["Id"].ToString()),
                        });
                    }
                }
            }

            return orders;
        }
    }
}
