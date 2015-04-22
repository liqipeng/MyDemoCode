using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Model
{
    public  class DepartmentService
    {
        IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public bool Insert(Department entity) 
        {
            return this._departmentRepository.Insert(entity);
        }

        public bool Update(Department entity) 
        {
            return this._departmentRepository.Update(entity);
        }

        public bool DeleteById(int id) 
        {
            return this._departmentRepository.DeleteById(id);
        }

        public IList<Department> GetAll() 
        {
            return this._departmentRepository.GetAll();
        }
    }
}
