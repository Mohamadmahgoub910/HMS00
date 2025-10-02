using HMS00.Data;
using HMS00.Models;
using Microsoft.AspNetCore.Mvc;

namespace HMS00.Controllers
{
    public class UsersController : Controller
    {
        private readonly HMSDBContext _context;

        public UsersController(HMSDBContext context)
        {
            _context = context;
        }

        // GET: /Users/All
        public IActionResult All()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // GET: /Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            _context.Users.Add(user);
            _context.SaveChanges();

            TempData["Success"] = "User created successfully!";
            return RedirectToAction("All");
        }

        // GET: /Users/Edit/5
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            _context.Users.Update(user);
            _context.SaveChanges();

            TempData["Success"] = "User updated successfully!";
            return RedirectToAction("All");
        }

        // GET: /Users/Delete/5
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            _context.SaveChanges();

            TempData["Success"] = "User deleted successfully!";
            return RedirectToAction("All");
        }
    }
}
