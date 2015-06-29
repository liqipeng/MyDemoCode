using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.MultipleDB
{
    public static class Initializer
    {
        public static void Initialize() 
        {
            //设置DataDirectory文件夹
            SetDataDir();
        }

        /// <summary>
        /// 解决provider不能自动加载的问题
        /// （不需要调用，放在这里即可）
        /// </summary>
        private static void FixProvidersNotAutoLoadProblem() 
        {
            var _ = typeof(System.Data.SQLite.EF6.SQLiteProviderFactory);
            var __ = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            var ___ = typeof(System.Data.Entity.SqlServerCompact.SqlCeProviderServices);
        }

        private static void SetDataDir() 
        {
            DirectoryInfo baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string data_dir = baseDir.FullName;
            if ((baseDir.Name.ToLower() == "debug" || baseDir.Name.ToLower() == "release")
                && (baseDir.Parent.Name.ToLower() == "bin"))
            {
                data_dir = Path.Combine(baseDir.Parent.Parent.FullName, "App_Data");
            }
            
            AppDomain.CurrentDomain.SetData("DataDirectory", data_dir);
        }
    }
}
