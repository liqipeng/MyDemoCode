using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Service
{
    public class UserViewModel
    {
        public long Id { get; set; }

        public string DisplayName { get; set; }

        public string Gender { get; set; }

        public string CreationTime { get; set; }

        public string EmployeeNumber { get; set; }

        public string Department { get; set; }
    }
}
