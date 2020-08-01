using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Health_Meter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Health_Meter.Models;
using Microsoft.EntityFrameworkCore;

namespace Health_Meter.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(
            [Bind("Name,Age,Height,Weight,Fnumber")]Person person)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(person);
                    await _context.SaveChangesAsync();
                    Debug.WriteLine(person.Name+"?????????????????????");
                    Debug.WriteLine(person.Fnumber+"~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    return RedirectToAction(nameof(Result_page), new
                    {
                        person.Name, person.Age, person.Fnumber,
                        person.Height, person.Weight
                    });
                }
            }
            catch (DbUpdateException /* ex */)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again or talk to your developer");
            }


            return View();
        }
        public IActionResult Result_page(
            [Bind("Name,Age,Height,Weight,Fnumber")]Person person)
        {
            return View(person);
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
