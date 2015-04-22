using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using StructureMap.Configuration.DSL;
using ASPNETPatterns.Layered.Model;
using ASPNETPatterns.Layered.Repository;

namespace ASPNETPatterns.Layered.WebUI
{
    public class BootStrapper
    {
        public static void ConfigureStructureMap()
        {            
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<UserRegistry>();
                x.AddRegistry<DepartmentRegistry>();
            });
        }   
    }

    public class UserRegistry : Registry
    {
        public UserRegistry()
        {
            For<IUserRepository>().Use<UserRepository>(); 
        }
    }

    public class DepartmentRegistry : Registry
    {
        public DepartmentRegistry()
        {
            For<IDepartmentRepository>().Use<DepartmentRepository>(); 
        }
    }

}
