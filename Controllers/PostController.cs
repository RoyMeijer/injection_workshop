using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Workshop_Injection.Controllers {
    public class PostController : Controller {
        DatabaseContext databaseContext = new DatabaseContext();
        
        public IActionResult Index() {
            List<Post> posts = new List<Post>();
            posts = databaseContext.Posts.ToList();
            return View(posts);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Post post) {
            if (ModelState.IsValid) {
                databaseContext.Database.ExecuteSqlRaw(
                    $"INSERT INTO Posts (Title, Content) VALUES ('{post.Title}','{post.Content}')");
                databaseContext.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}