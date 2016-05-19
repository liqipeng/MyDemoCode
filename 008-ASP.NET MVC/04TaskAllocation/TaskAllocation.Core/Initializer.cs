using log4net.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAllocation.Core.DAL;

namespace TaskAllocation.Core
{
    public static class Initializer
    {
        public static void Initialize() 
        {
            //设置DataDirectory文件夹
            SetDataDir();
            SetLog4NetConfigFilePath();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TaskAllocationDBContext, Configuration>());
        }

        /// <summary>
        /// 解决provider不能自动加载的问题
        /// （不需要调用，放在这里即可）
        /// </summary>
        private static void FixProvidersNotAutoLoadProblem() 
        {
            var ___ = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
            var __ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
        }

        private static void SetLog4NetConfigFilePath() 
        {
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));
        }

        private static void SetDataDir() 
        {
            //DirectoryInfo baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            //string data_dir = baseDir.FullName;
            //if ((baseDir.Name.ToLower() == "debug" || baseDir.Name.ToLower() == "release")
            //    && (baseDir.Parent.Name.ToLower() == "bin"))
            //{
            //    data_dir = Path.Combine(baseDir.Parent.Parent.FullName, "App_Data");
            //}
            
            //AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(""));
        }
    }
}
