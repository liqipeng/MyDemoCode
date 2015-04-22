using ASPNETPatterns.DAL.ProxyPattern.Model;
using ASPNETPatterns.DAL.ProxyPattern.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNETPatterns.DAL.ProxyPattern.WebUI
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false) 
            {
                CustomerService customerService = new CustomerService(new CustomerRepository(new OrderRepository()));
                Customer customer = customerService.FindBy(Guid.NewGuid());

                this.Label1.Text = customer.Id.ToString();
                this.Label2.Text = customer.Name;

                this.Repeater1.DataSource = customer.Orders;
                this.Repeater1.DataBind();
            }
        }
    }
}