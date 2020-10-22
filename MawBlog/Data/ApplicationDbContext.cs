using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MawBlog.Models;

namespace MawBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MawBlog.Models.Blog> Blog { get; set; }
        public DbSet<MawBlog.Models.Comment> Comment { get; set; }
        public DbSet<MawBlog.Models.Post> Post { get; set; }
        public DbSet<MawBlog.Models.Tag> Tag { get; set; }
    }
}
