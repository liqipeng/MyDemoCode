using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03SearchPdf.Core
{
   public abstract  class Helper
    {

        public event Action<int> OnProgressChanged;

        protected void RaiseProgressChanged(int progressPercent)
        {
            if (OnProgressChanged != null)
            {
                OnProgressChanged(progressPercent);
            }
        }

        public event Action<string> HasLog;

        protected void WriteLog(string format, params object[] args)
        {
            WriteLog(string.Format(format, args));
        }

        protected void WriteLog(string log)
        {
            if (HasLog != null)
            {
                HasLog(log);
            }
        }
    }
}
