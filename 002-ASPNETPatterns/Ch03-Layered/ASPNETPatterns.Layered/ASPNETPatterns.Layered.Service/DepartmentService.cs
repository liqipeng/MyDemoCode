using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public  class DepartmentService
    {
        private Model.DepartmentService _departmentService;

        public DepartmentService (Model.DepartmentService departmentService)
	    {
            _departmentService = departmentService;
	    }

        public GetAllDepartmentResponse GetAll()
        {
            GetAllDepartmentResponse response = new GetAllDepartmentResponse();

            try
            {
                IList<Department> lstDepartment = _departmentService.GetAll();

                response.List = lstDepartment.MapToDepartmentViewModelList();
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
