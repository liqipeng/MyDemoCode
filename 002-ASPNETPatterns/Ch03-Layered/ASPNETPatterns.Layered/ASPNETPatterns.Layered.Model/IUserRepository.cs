using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Model
{
    public interface IUserRepository
    {
        bool Insert(User entity);

        bool Update(User entity);

        bool DeleteById(long id);

        bool GetById(long id);

        IList<User> GetAll();
    }
}
