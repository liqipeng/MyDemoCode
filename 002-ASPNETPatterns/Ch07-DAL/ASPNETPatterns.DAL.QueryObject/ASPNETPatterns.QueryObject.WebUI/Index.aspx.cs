using ASPNETPatterns.QueryObject.Model;
using ASPNETPatterns.QueryObject.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNETPatterns.QueryObject.WebUI
{
    public partial class Index : System.Web.UI.Page
    {
        private OrderService _orderService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) 
            {
                string connectionString = ConfigurationManager.ConnectionStrings["QueryObject"].ConnectionString;
                _orderService = new OrderService(new OrderRepository(connectionString));

                var orders = _orderService.FindAllCustomerOrdersWithInOrderDateBy(Guid.NewGuid(), DateTime.Now);

                this.Repeater1.DataSource = orders;
                this.Repeater1.DataBind();
            }
        }
    }
}