using ASPNETPatterns.DAL.UnitOfWork.Model;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNETPatterns.DAL.UnitOfWork.WebUI
{
    public partial class Transfer : System.Web.UI.Page
    {
        private AccountService _accountService;
        private Account account1;
        private Account account2;

        protected void Page_Init(object sender, EventArgs e)
        {
            this._accountService = ObjectFactory.GetInstance<AccountService>();

            this.account1 = this._accountService.GetAccountById(1);
            this.account2 = this._accountService.GetAccountById(2);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                this.TextBox1.Text = account1.Balance.ToString("n");
                this.TextBox2.Text = account2.Balance.ToString("n");
            }
        }

        protected void btnAToB_Click(object sender, EventArgs e)
        {
            decimal transferAmount = Convert.ToDecimal(this.txtTransferAmount.Text.Trim());
            this._accountService.Transfer(account1, account2, transferAmount);

            Response.Redirect(Request.Url.ToString()); 
        }

        protected void btnBToA_Click(object sender, EventArgs e)
        {
            decimal transferAmount = Convert.ToDecimal(this.txtTransferAmount.Text.Trim());
            this._accountService.Transfer(account2, account1, transferAmount);

            Response.Redirect(Request.Url.ToString()); 
        }
    }
}