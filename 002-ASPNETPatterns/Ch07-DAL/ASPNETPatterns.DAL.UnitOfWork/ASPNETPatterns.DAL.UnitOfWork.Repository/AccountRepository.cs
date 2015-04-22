using ASPNETPatterns.DAL.UnitOfWork.Infrastructure;
using ASPNETPatterns.DAL.UnitOfWork.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASPNETPatterns.DAL.UnitOfWork.Repository
{
    public class AccountRepository : IAccountRepository, IUnitOfWorkRepository 
    {
        private static readonly string _connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private IUnitOfWork _unitOfWork;

        public AccountRepository(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public void Save(Account account)
        {
            _unitOfWork.RegisterAmended(account, this);
        }

        public void Add(Account account)
        {
            _unitOfWork.RegisterNew(account, this);
        }

        public void Remove(Account account)
        {
            _unitOfWork.RegisterRemoved(account, this);
        }

        public void PersistCreationOf(IAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public void PersistUpdateOf(IAggregateRoot entity)
        {
            Account account = entity as Account;

            string sql = "UPDATE Account SET Balance=@Balance WHERE Id=@Id";
            SqlParameter parameter1 = new SqlParameter("@Id", account.Id);
            SqlParameter parameter2= new SqlParameter("@Balance", account.Balance);

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(parameter1);
                    cmd.Parameters.Add(parameter2);

                    int affectedCount = cmd.ExecuteNonQuery();

                    if (affectedCount <= 0) 
                    {
                        throw new Exception("update Account failed");
                    }
                }
            }
        }

        public void PersistDeletionOf(IAggregateRoot entity)
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            string sql = "SELECT Id, Balance FROM Account WHERE Id=@Id";
            SqlParameter parameter = new SqlParameter("@Id", id);
            Account account = new Account();

            using (SqlConnection conn = new SqlConnection(_connStr))
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.Add(parameter);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read()) 
                    {
                        account.Id = id;
                        account.Balance = Convert.ToDecimal(reader["Balance"]);
                    }
                }
            }

            return account;
        }
    }
}
