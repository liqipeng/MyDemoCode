using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public static class DepartmentMapperExtensionMethods
    {
        public static IList<DepartmentViewModel> MapToDepartmentViewModelList(this IList<Department> lstDepartment)
        {
            IList<DepartmentViewModel> lstDepartmentViewModel = new List<DepartmentViewModel>();

            foreach (var item in lstDepartment) 
            {
                lstDepartmentViewModel.Add(item.MapToViewModel());
            }

            return lstDepartmentViewModel;
        }

        public static DepartmentViewModel MapToViewModel(this Department entity) 
        {
            DepartmentViewModel viewModel = new DepartmentViewModel();
            viewModel.Id = entity.Id;
            viewModel.Name = entity.Name;
            viewModel.Description = entity.Description;

            return viewModel;
        }

    }
}
