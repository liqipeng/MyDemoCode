using ASPNETPatterns.Layered.Presentation;
using ASPNETPatterns.Layered.Service;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNETPatterns.Layered.WebUI
{
    public partial class Department : System.Web.UI.Page, IListDepartmentView
    {
        private DepartmentPresenter _presenter;

        public void Page_Init(object sender, EventArgs e) 
        {
            _presenter = new DepartmentPresenter(this, ObjectFactory.GetInstance<DepartmentService>());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this._presenter.Display();
            }
        }

        public void Display(IList<DepartmentViewModel> lstDepartmentViewModel)
        {
            this.rptDepartments.DataSource = lstDepartmentViewModel;
            this.rptDepartments.DataBind();
        }

        public string ErrorMessage
        {
            set { lblErrorMessage.Text = string.Format("加载数据出错：{0}", value); }
        }
    }
}