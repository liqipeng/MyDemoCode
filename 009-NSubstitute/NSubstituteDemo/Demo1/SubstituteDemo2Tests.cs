using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Demo1.Biz;
using Demo1.Interface;
using NSubstitute;
using Demo1.Entity;

namespace Demo1
{
    [TestClass]
    public class SubstituteDemo2Tests
    {
        [TestMethod]
        public void TestAddPerson()
        {
            //Arrange
            ILog log = Substitute.For<ILog>();
            IDAL<Person, int> personDAL = Substitute.For<IDAL<Person, int>>();
            IPersonManager personManager = new PersonManager(log, personDAL);

            //Act
            Guid key = Guid.NewGuid();
            personManager.AddPerson("Tom", 18, key);

            //Assert
            log.Received().Info(Arg.Any<Log>());
            personDAL.Received<IDAL<Person, int>>()
                .Insert<Person>(Arg.Is<Person>(p => !string.IsNullOrWhiteSpace(p.Name) 
                    && p.Age > 0
                    && p.Key == key));
        }
    }
}
