using HMS00.Data;
using HMS00.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HMS00.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly HMSDBContext _context;

        public AppointmentsController(HMSDBContext context)
        {
            _context = context;
        }

        // GET: /Appointments/All
        public IActionResult All()
        {
            var appointments = _context.Appointments
                .Include(a => a.User)
                .Include(a => a.Doctor)
                .OrderBy(a => a.AppointmentDate)
                .ThenBy(a => a.AppointmentTime)
                .ToList();

            return View(appointments);
        }

        // GET: /Appointments/Create
        public IActionResult Create(int? doctorId)
        {
            LoadDropDowns(doctorId);

            var appointment = new Appointment
            {
                AppointmentDate = DateTime.Today.AddDays(1)
            };

            if (doctorId.HasValue)
            {
                appointment.DoctorId = doctorId.Value;
                var doctor = _context.Doctors.Find(doctorId.Value);
                ViewBag.SelectedDoctorName = doctor?.Name;
            }

            return View(appointment);
        }

        // POST: /Appointments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Appointment appointment, string AppointmentTimeStr)
        {
            if (!string.IsNullOrEmpty(AppointmentTimeStr))
            {
                // تحويل الوقت من string لـ TimeSpan
                appointment.AppointmentTime = TimeSpan.Parse(AppointmentTimeStr);
            }

            _context.Appointments.Add(appointment);
            _context.SaveChanges();

            TempData["Success"] = "Your appointment has been successfully Submitted, Thank you!";
            return RedirectToAction("Create");
        }

        // GET: /Appointments/GetAvailableTimes
        [HttpGet]
        public JsonResult GetAvailableTimes(int doctorId, DateTime date)
        {
            var allTimes = GenerateTimeSlots();

            var bookedTimes = _context.Appointments
                .Where(a => a.DoctorId == doctorId && a.AppointmentDate == date)
                .Select(a => a.AppointmentTime)
                .ToList();

            var availableTimes = allTimes
                .Where(t => !bookedTimes.Contains(t))
                .Select(t => new {
                    Value = t.Hours.ToString("D2") + ":" + t.Minutes.ToString("D2"),
                    Text = t.Hours.ToString("D2") + ":" + t.Minutes.ToString("D2")
                })
                .ToList();

            return Json(availableTimes);
        }

        private List<TimeSpan> GenerateTimeSlots()
        {
            var times = new List<TimeSpan>();
            for (int hour = 9; hour <= 17; hour++)
            {
                times.Add(new TimeSpan(hour, 0, 0));
                if (hour < 17) times.Add(new TimeSpan(hour, 30, 0));
            }
            return times;
        }

        private void LoadDropDowns(int? selectedDoctorId)
        {
            ViewBag.Users = _context.Users.ToList();
            ViewBag.Doctors = _context.Doctors.ToList();
            ViewBag.SelectedDoctorId = selectedDoctorId;
        }
    }
}
