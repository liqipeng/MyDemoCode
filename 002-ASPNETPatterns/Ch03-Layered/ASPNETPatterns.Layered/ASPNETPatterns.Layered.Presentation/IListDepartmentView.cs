using ASPNETPatterns.Layered.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Presentation
{
    public interface IListDepartmentView
    {
        void Display(IList<DepartmentViewModel> lstDepartmentViewModel);

        string ErrorMessage { set; }
    }
}
