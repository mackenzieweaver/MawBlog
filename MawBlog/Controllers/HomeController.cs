using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MawBlog.Models;
using MawBlog.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Post.Include(p => p.Blog);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Results(string SearchString)
        {
            var posts = from p in _context.Post
                        select p;
            if (!String.IsNullOrEmpty(SearchString))
            {
                posts = posts.Where(p => p.Title.Contains(SearchString) || p.Abstract.Contains(SearchString) || p.Content.Contains(SearchString));                
                return View("Index", await posts.ToListAsync());
            }
            return View("Index", await posts.ToListAsync());
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
