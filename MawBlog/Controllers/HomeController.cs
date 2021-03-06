﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MawBlog.Models;
using MawBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore;
using System.IO;
using System.Collections;
using MawBlog.ViewModels;

namespace MawBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;        

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page)
        {
            var posted = _context.Post.Where(p => p.IsPublished).ToList().Count;
            if (page < 0)
            {
                page = 0;
            }
            if(page * 5 > _context.Post.ToList().Count)
            {
                page = _context.Post.ToList().Count / 5;
            }
            var posts = _context.Post
                .Where(p => p.IsPublished)
                .OrderByDescending(p => p.Created)
                .Include(p => p.Blog)
                .Skip(page * 5).Take(5);
            var blogs = _context.Blog;
            var tags = _context.Tag;
            CategoriesVM categories = new CategoriesVM()
            {
                Blogs = await blogs.ToListAsync(),
                Posts = await posts.ToListAsync(),
                Tags = await tags.ToListAsync(),
                PageNum = page,
                TotalPosts = _context.Post.ToList().Count
            };
            return View(categories);
        }

        public async Task<IActionResult> Results(string SearchString)
        {
            var posts = from p in _context.Post
                        select p;
            var blogs = _context.Blog;
            var tags = _context.Tag;
            if (!String.IsNullOrEmpty(SearchString))
            {
                posts = posts.Where(p => p.Title.Contains(SearchString) || p.Abstract.Contains(SearchString) || p.Content.Contains(SearchString));
                //return View("Index", await posts.Include(p => p.Blog).ToListAsync());
            }
            //return View("Index", await posts.Include(p => p.Blog).ToListAsync());
            CategoriesVM categories = new CategoriesVM()
            {
                Blogs = await blogs.ToListAsync(),
                Posts = await posts.ToListAsync(),
                Tags = await tags.ToListAsync()
            };
            return View("Index", categories);
        }

        public async Task<IActionResult> Categories()
        {
            var id = RouteData.Values["id"].ToString();
            var posts = _context.Post.Where(p => p.BlogId == Int32.Parse(id) && p.IsPublished == true).Include(p => p.Blog);
            var blogs = _context.Blog;
            var tags = _context.Tag;
            CategoriesVM categories = new CategoriesVM()
            {
                Blogs = await blogs.ToListAsync(),
                Posts = await posts.ToListAsync(),
                Tags = await tags.ToListAsync()
            };
            return View("Index", categories);
        }

        public async Task<IActionResult> Tag()
        {
            var name = RouteData.Values["id"].ToString();
            var posts = _context.Tag.Where(t => t.Name == name).Select(t => t.Post);
            var blogs = _context.Blog;
            CategoriesVM categories = new CategoriesVM()
            {
                Blogs = await blogs.ToListAsync(),
                Posts = await posts.ToListAsync(),
                Tags = await _context.Tag.ToListAsync()
            };
            return View("Index", categories);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
