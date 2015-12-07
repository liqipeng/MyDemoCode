using Demo1.Entity;
using Demo1.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo1.Biz
{
    public class PersonDAL : IDAL<Person, int>
    {
        private ILog _log;

        public PersonDAL(ILog log)
        {
            this._log = log;
        }

        public Person FindByKey(int key)
        {
            this._log.Info(new Log() { Content = "select person by key=" + key });

            return new Person()
            {
                Id = key,
                Name = "Zhangsan",
                Age = 12
            };
        }


        public int Insert<T>(T entity)
        {
            this._log.Info(new Log() { Content = "insert a new person record to db" });

            return 100;
        }
    }
}
