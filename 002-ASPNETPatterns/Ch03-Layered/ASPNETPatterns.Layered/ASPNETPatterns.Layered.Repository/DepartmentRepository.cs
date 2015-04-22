using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public bool Insert(Department entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(Department entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool GetById(int id) 
        {
            throw new NotImplementedException();
        }

        public IList<Department> GetAll()
        {
            return new List<Department>() 
            {
                new Department()
                {
                    Id = 1,
                    Name = "IT部",
                    Description = "负责企业IT建设等"
                }
            };
        }
    }
}
