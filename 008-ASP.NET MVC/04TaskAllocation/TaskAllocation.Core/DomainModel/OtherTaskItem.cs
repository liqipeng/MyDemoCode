using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocation.Core.DomainModel
{
    [Table("OtherTaskItems")]
    public class OtherTaskItem : TaskItem
    {
        public String OtherName { get; set; }
        public String Comment { get; set; }
    }
}
