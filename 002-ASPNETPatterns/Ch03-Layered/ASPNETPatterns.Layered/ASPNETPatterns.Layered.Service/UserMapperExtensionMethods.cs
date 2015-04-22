using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public static class UserMapperExtensionMethods
    {
        public static IList<UserViewModel> MapToDepartmentViewModelList(this IList<User> lstDepartment)
        {
            IList<UserViewModel> lstDepartmentViewModel = new List<UserViewModel>();

            foreach (var item in lstDepartment) 
            {
                lstDepartmentViewModel.Add(item.MapToViewModel());
            }

            return lstDepartmentViewModel;
        }

        public static UserViewModel MapToViewModel(this User entity) 
        {
            UserViewModel viewModel = new UserViewModel();
            viewModel.Id = entity.Id;
            viewModel.DisplayName = string.Format("{0} {1}", entity.FamilyName, entity.LastName);
            viewModel.Gender = entity.Gender.GetDisplayName();
            viewModel.CreationTime = entity.CreationTime.Value != null ? entity.CreationTime.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
            viewModel.EmployeeNumber = entity.EmployeeNumber;
            viewModel.Department = entity.Department.Name;

            return viewModel;
        }

    }
}
