using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public class GetAllDepartmentResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public IList<DepartmentViewModel> List { get; set; }
    }
}
