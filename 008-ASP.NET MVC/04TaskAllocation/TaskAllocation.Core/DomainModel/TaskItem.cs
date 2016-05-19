using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace TaskAllocation.Core.DomainModel
{
    public abstract class TaskItem
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 TaskId { get; set; }

        [ForeignKey("TaskId")]
        public virtual Task Task { get; set; }
    }
}
