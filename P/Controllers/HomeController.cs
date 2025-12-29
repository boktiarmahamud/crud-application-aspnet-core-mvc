using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P.Models;

namespace P.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDbContext studentDb;

        public HomeController(StudentDbContext StudentDb)
        {
            studentDb = StudentDb;
        }
        public async Task<IActionResult> Index()
        {
            var std = await studentDb.StudentsInformation.ToListAsync();
            return View(std);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student std)
        {
            if (ModelState.IsValid)
            {
                await studentDb.StudentsInformation.AddAsync(std);
                await studentDb.SaveChangesAsync();
                TempData["success"] = "Student Created Successfully";
                return RedirectToAction("Index","Home");
            }
            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            if(id == null || studentDb.StudentsInformation == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.StudentsInformation.FirstOrDefaultAsync(x => x.Id == id);
            if(stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null || studentDb.StudentsInformation == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.StudentsInformation.FindAsync(id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Student std)
        {
            if (id != std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDb.Update(std);
                await studentDb.SaveChangesAsync();
                TempData["Edit_successful"] = "Edited Successfully";
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || studentDb.StudentsInformation == null)
            {
                return NotFound();
            }
            var stdData = await studentDb.StudentsInformation.FirstOrDefaultAsync(x => x.Id == id);
            if (stdData == null)
            {
                return NotFound();
            }

            return View(stdData);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            var stdData = await studentDb.StudentsInformation.FindAsync(id);
            if (stdData != null)
            {
                studentDb.StudentsInformation.Remove(stdData);
            }
            await studentDb.SaveChangesAsync();
            TempData["delete_successful"] = "Deleted Successfully";
            return RedirectToAction("Index", "Home");
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
