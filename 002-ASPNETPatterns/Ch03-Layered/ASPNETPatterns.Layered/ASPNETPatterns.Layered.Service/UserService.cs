using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public class UserService
    {
        private Model.UserService _userService;

        public UserService(Model.UserService userService)
        {
            this._userService = userService;
        }

        public GetAllUserResponse GetAll() 
        {
            GetAllUserResponse response = new GetAllUserResponse();

            try
            {
                response.Success = true;
                response.List = this._userService.GetAll().MapToDepartmentViewModelList();
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
