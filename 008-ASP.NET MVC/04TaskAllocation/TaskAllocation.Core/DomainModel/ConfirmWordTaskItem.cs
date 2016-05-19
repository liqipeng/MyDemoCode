using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocation.Core.DomainModel
{
    [Table("ConfirmWordTaskItems")]
    public class ConfirmWordTaskItem : TaskItem
    {
        public String Word { get; set; }
        public String Comment { get; set; }
    }
}
