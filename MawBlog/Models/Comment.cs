using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.Models
{
    public class Comment
    {
        // Id (primary key)
        public int Id { get; set; }
        // wordId (foreign key)
        public int PostId { get; set; }
        public string AuthorId { get; set; }

        public string Content { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        #region Navigation
        // Have a navigation for each foriegn key
        //public Post Post { get; set; }
        public virtual Post Post { get; set; }
        //public BlogUser Author { get; set; }
        public virtual BlogUser Author { get; set; }
        #endregion
    }
}
