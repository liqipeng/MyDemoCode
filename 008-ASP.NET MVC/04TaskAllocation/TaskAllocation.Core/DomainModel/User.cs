using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskAllocation.Core.DomainModel
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Int32 Id { get; set; }
        public String Name { get; set; }

        public String Password { get; set; }
    }
}
