using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocation.Core.Common
{
    public class DataGridModel<T>
    {
        public DataGridModel()
        {
            this.Rows = new List<T>();
        }

        public DataGridModel(int total, List<T> lstEntities)
        {
            this.Total = total;
            this.Rows = lstEntities;
        }

        public int Total { get; set; }
        public List<T> Rows { get; set; }

        public Object ToDataObject() 
        {
            if (this.Rows == null) 
            {
                this.Rows = new List<T>();
            }

            return new { total = Total, rows = Rows };
        }
    }
}
