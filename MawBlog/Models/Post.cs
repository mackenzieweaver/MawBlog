using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.Models
{
    public class Post
    {
        public int Id { get; set; }
        /// <summary>
        /// foreign key BlogId equals Id they're the same value
        /// </summary>
        public int BlogId { get; set; }

        public string Title { get; set; }
        public string Abstract { get; set; }

        public string Content { get; set; }
        public byte[] Image { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public Blog Blog { get; set; }
        public List<Comment> Comments{ get; set; }
        public List<Tag> Tags { get; set; }

        public Post()
        {
            Comments = new List<Comment>();
            Tags = new List<Tag>();
        }

    }
}
