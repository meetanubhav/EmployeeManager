using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DBContext _db;

        [BindProperty]
        public EmployeeModel Employee { get; set; }

        public EmployeeController(DBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Update(int? id)
        {
            Employee = new EmployeeModel();
            if (id == null)
            {
                return View(Employee);
            }
            Employee = _db.Employee.FirstOrDefault(u => u.ID == id);
            if (Employee == null)
            {
                return NotFound();
            }
            return View(Employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {

            if (ModelState.IsValid)
            {
                if (Employee.ID == 0)
                {
                    _db.Employee.Add(Employee);
                }
                else
                {
                    _db.Employee.Update(Employee);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }


            return View(Employee);
        }


        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Employee.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var EmployeeFromDb = await _db.Employee.FirstOrDefaultAsync(u => u.ID == id);
            if (EmployeeFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Employee.Remove(EmployeeFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}
