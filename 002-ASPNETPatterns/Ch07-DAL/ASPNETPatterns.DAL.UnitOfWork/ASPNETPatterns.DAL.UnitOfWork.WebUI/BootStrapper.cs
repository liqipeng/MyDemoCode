using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using ASPNETPatterns.DAL.UnitOfWork.Model;
using ASPNETPatterns.DAL.UnitOfWork.Repository;
using ASPNETPatterns.DAL.UnitOfWork.Infrastructure;

namespace ASPNETPatterns.DAL.UnitOfWork.WebUI
{
    public class BootStrapper
    {
        public static void ConfigureStructureMap()
        {            
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<AccountRepositoryRegistry>();
                x.AddRegistry<UnitOfWorkRegistry>();
            });
        }   
    }

    public class AccountRepositoryRegistry : Registry
    {
        public AccountRepositoryRegistry()
        {
            For<IAccountRepository>().Use<AccountRepository>(); 
        }
    }

    public class UnitOfWorkRegistry : Registry
    {
        public UnitOfWorkRegistry()
        {
            For<IUnitOfWork>().Use<ASPNETPatterns.DAL.UnitOfWork.Infrastructure.UnitOfWork>(); 
        }
    }

}
