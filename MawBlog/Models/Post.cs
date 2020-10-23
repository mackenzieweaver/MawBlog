using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string Slug { get; set; }
        public bool IsPublished { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
        public byte[] Image { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        #region Navigation        
        //public Blog Blog { get; set; }
        public virtual Blog Blog { get; set; }
        //public List<Comment> Comments{ get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        //public List<Tag> Tags { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        #endregion
        //public Post()
        //{
        //    Comments = new List<Comment>();
        //    Tags = new List<Tag>();
        //}
        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }
    }
}
