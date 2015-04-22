using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASPNETPatterns.Layered.Model;

namespace ASPNETPatterns.Layered.Model
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public bool Insert(User entity)
        {
            return this._userRepository.Insert(entity);
        }

        public bool Update(User entity)
        {
            return this._userRepository.Update(entity);
        }

        public bool DeleteById(long id)
        {
            return this._userRepository.DeleteById(id);
        }

        public IList<User> GetAll() 
        {
            return this._userRepository.GetAll();
        }
    }
}
