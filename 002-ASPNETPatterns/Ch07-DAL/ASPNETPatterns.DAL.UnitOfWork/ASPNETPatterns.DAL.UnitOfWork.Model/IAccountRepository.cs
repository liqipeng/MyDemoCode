using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASPNETPatterns.DAL.UnitOfWork.Model
{
 public interface IAccountRepository
 {
      void Save(Account account);
      void Add(Account account);
      void Remove(Account account);

      Account GetById(int id);
 }
}
