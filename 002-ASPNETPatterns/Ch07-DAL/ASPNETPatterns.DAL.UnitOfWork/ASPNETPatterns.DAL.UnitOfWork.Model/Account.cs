using ASPNETPatterns.DAL.UnitOfWork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.UnitOfWork.Model
{
    public class Account : IAggregateRoot
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
    }
}
