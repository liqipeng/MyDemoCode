using ASPNETPatterns.Layered.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Presentation
{
    public class DepartmentPresenter
    {
        private IListDepartmentView _listDepartmentView;
        private DepartmentService _departmentService;

        public DepartmentPresenter(IListDepartmentView lstDepartmentView, DepartmentService departmentService)
        {
            this._listDepartmentView = lstDepartmentView;
            this._departmentService = departmentService;
        }

        public void Display() 
        {
            var response = _departmentService.GetAll();

            if(response.Success)
            {
                this._listDepartmentView.Display(response.List);
            }
            else
            {
                this._listDepartmentView.ErrorMessage = response.Message;
            }
        }
    }
}
