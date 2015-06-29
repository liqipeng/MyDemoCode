using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDemo.MultipleDB.Models
{
    public class Post
    {
        public int PostId { get; set; }

        [StringLength(128)] 
        public string Title { get; set; }

        [DataType(DataType.Text)] 
        public string Content { get; set; }

        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }
    }
}
