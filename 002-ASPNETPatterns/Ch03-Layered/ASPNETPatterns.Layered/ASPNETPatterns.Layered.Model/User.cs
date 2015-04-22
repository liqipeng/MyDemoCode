using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Model
{
    public class User
    {
        public long Id { get; set; }

        public string FamilyName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime? CreationTime { get; set; }

        public string EmployeeNumber { get; set; }

        public Department Department { get; set; }
    }
}
