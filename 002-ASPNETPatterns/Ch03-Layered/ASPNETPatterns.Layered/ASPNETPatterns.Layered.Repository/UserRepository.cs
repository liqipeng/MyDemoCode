using ASPNETPatterns.Layered.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.Layered.Repository
{
    public class UserRepository : IUserRepository
    {
        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(long id)
        {
            throw new NotImplementedException();
        }

        public bool GetById(long id)
        {
            throw new NotImplementedException();
        }

        public IList<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
