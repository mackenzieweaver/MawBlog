using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Name { get; set; }
        public Post Post { get; set; }
    }
}
