using HMS00.Data;
using HMS00.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HMS00.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly HMSDBContext _context;

        public DoctorsController(HMSDBContext context)
        {
            _context = context;
        }

        // GET: /Doctors
        public IActionResult Index()
        {
            var doctors = _context.Doctors.ToList();
            return View(doctors);
        }

        // GET: /Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Doctors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Doctors.Add(doctor);
                _context.SaveChanges();
                TempData["Success"] = "Doctor created successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: /Doctors/Edit/5
        public IActionResult Edit(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        // POST: /Doctors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Doctor doctor)
        {
            if (id != doctor.DoctorId) return BadRequest();

            if (ModelState.IsValid)
            {
                _context.Update(doctor);
                _context.SaveChanges();
                TempData["Success"] = "Doctor updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: /Doctors/Delete/5
        public IActionResult Delete(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }

        // POST: /Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
                _context.SaveChanges();
                TempData["Success"] = "Doctor deleted successfully.";
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: /Doctors/Details/5
        public IActionResult Details(int id)
        {
            var doctor = _context.Doctors.Find(id);
            if (doctor == null) return NotFound();
            return View(doctor);
        }
    }
}
