using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.ProxyPattern.Model
{
    public  class CustomerService
    {
        private ICustomerRepository _cusomterRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _cusomterRepository = customerRepository; 
        }

        public Customer FindBy(Guid id) 
        {
            return _cusomterRepository.FindBy(id);
        }
    }
}
