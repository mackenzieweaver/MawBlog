using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        public string DisplayName { get; set; }

        //public List<Comment> Comments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        // Constructor
        public BlogUser()
        {
            //Comments = new List<Comment>();
            Comments = new HashSet<Comment>();
        }
    }
}
