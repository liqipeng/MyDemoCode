using Demo1.Entity;
using Demo1.Interface;
using System;

namespace Demo1.Biz
{
    public class PersonManager : IPersonManager
    {
        private ILog _log;
        private IDAL<Person, int> _personDAL;

        public PersonManager(ILog log, IDAL<Person, int> personDAL)
        {
            this._log = log;
            this._personDAL = personDAL;
        }

        public void AddPerson(string name, int age)
        {
            this._personDAL.Insert(new Person() { Name = name, Age = age });

            this._log.Info(new Log() { Content = "add a new person." });
        }
    }
}
