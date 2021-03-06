﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MawBlog.Data;
using MawBlog.Models;
using MawBlog.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Xml;

namespace MawBlog.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Comments
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comment.Include(c => c.Author).Include(c => c.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Author)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        [Authorize(Roles = "Admin, Moderator")]
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<BlogUser>(), "Id", "FirstName");
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Title");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostId")] Comment comment, string userComment)
        {
            if (ModelState.IsValid)
            {
                var email = HttpContext.User.Identity.Name;
                var authorid = _context.Users.FirstOrDefault(u => u.Email == email).Id;
                var author = _context.Users.FirstOrDefault(u => u.Email == email);
                var post = _context.Post.FirstOrDefault(p => p.Id == comment.PostId);

                comment.Created = DateTime.Now;
                comment.Updated = DateTime.Now;
                comment.Content = userComment;
                comment.AuthorId = authorid;
                comment.Author = author;
                comment.Post = post;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                //return Redirect($"~/Posts/Details/{comment.PostId}");
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<BlogUser>(), "Id", "Id", comment.AuthorId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<BlogUser>(), "Id", "DisplayName", comment.AuthorId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Title", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,AuthorId,Content,Created,Updated")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (comment.Content.Contains("<!DOCTYPE"))
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(comment.Content);
                        XmlNode elem = doc.DocumentElement.FirstChild.NextSibling;
                        comment.Content = elem.FirstChild.InnerText.ToString();
                    }
                    comment.Post = _context.Post.FirstOrDefault(p => p.Id == comment.PostId);
                    comment.Author = _context.Users.FirstOrDefault(u => u.Id == comment.AuthorId);
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Details", "Posts", new { id = comment.PostId });
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<BlogUser>(), "Id", "Id", comment.AuthorId);
            ViewData["PostId"] = new SelectList(_context.Post, "Id", "Id", comment.PostId);
            //return View(comment);
            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.Author)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
