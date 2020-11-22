using MawBlog.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MawBlog.ViewModels
{
    public class CategoriesVM
    {
        public ICollection<Blog> Blogs { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public int PageNum { get; set; }
        public int TotalPosts { get; set; }
    }
}
