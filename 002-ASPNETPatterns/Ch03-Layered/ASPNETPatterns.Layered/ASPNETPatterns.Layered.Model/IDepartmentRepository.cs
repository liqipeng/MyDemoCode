using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Model
{
    public interface IDepartmentRepository
    {
        bool Insert(Department entity);

        bool Update(Department entity);

        bool DeleteById(int id);

        bool GetById(int id);

        IList<Department> GetAll();
    }
}
