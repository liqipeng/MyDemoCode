using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.ProxyPattern.Model
{
    public interface ICustomerRepository
    {
        Customer FindBy(Guid id);
    }
}
