using HMS00.Data;
using HMS00.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HMS00.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HMSDBContext _context;  

        public HomeController(ILogger<HomeController> logger, HMSDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                WelcomeMessage = "Welcome to Our Hospital",
                Specialties = new List<string>
                {
                    "Internal Medicine",
                    "Pediatrics",
                    "Surgery"
                }
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ViewResult PersonalInfo()
        {
            return View();
        }

        //  Book Appointment
        public IActionResult BookAppointment(string searchTerm, int page = 1, int pageSize = 5)
        {
            var query = _context.Doctors.AsQueryable();

            
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => d.Name.Contains(searchTerm) || d.Specialization.Contains(searchTerm));
            }

            // Pagination
            var totalItems = query.Count();
            var doctors = query
                            .OrderBy(d => d.Name)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = totalItems;
            ViewBag.SearchTerm = searchTerm;

            return View(doctors);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
