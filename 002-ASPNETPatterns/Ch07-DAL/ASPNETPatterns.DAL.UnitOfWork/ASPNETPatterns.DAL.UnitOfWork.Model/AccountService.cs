using ASPNETPatterns.DAL.UnitOfWork.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.UnitOfWork.Model
{
    public class AccountService
    {
        private IAccountRepository _accountRepository;
        private IUnitOfWork _unitOfWork;

        public AccountService(IAccountRepository accountRepository, IUnitOfWork unitOfWork)
        {
            this._accountRepository = accountRepository;
            this._unitOfWork = unitOfWork;
        }

        public Account GetAccountById(int id) 
        {
            return this._accountRepository.GetById(id);
        }

        public void Transfer(Account from, Account to, decimal amount) 
        {
            if (from.Balance > amount)
            {
                from.Balance -= amount;
                to.Balance += amount;

                this._accountRepository.Save(from);
                this._accountRepository.Save(to);

                this._unitOfWork.Commit();
            }
            else 
            {
                throw new Exception("余额不足");
            }
        }
    }
}
