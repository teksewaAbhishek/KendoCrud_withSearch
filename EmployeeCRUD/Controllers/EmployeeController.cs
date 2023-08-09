using EmployeeCRUD.Data;
using EmployeeCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchBy, string searchValue)
        {
            IEnumerable<Employee> objCatlist = _context.Employees;
           
            if(string.IsNullOrEmpty(searchValue))
            {
                
                return View(objCatlist);
            }
            else
            {
                if(searchBy.ToLower() == "name")
                {
                    var searchByName = objCatlist.Where(p => p.Name.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByName);
                }
                else if (searchBy.ToLower() == "designation")
                {
                    var searchByDesig = objCatlist.Where(p => p.Designation.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByDesig);
                }
                else if (searchBy.ToLower() == "address")
                {
                    var searchByAddress = objCatlist.Where(p => p.Address.ToLower().Contains(searchValue.ToLower()));
                    return View(searchByAddress);
                }
            }
            
            return View(objCatlist);
        }

        public IActionResult GetGridData()
        {
            IEnumerable<Employee> objCatlist1 = _context.Employees;
            var data = objCatlist1;
           
            return Json(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Add(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Record Added Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee empobj)
        {
            if (ModelState.IsValid)
            {
                _context.Employees.Update(empobj);
                _context.SaveChanges();
                TempData["ResultOk"] = "Data Updated Successfully !";
                return RedirectToAction("Index");
            }

            return View(empobj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var empfromdb = _context.Employees.Find(id);

            if (empfromdb == null)
            {
                return NotFound();
            }
            return View(empfromdb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deleterecord = _context.Employees.Find(id);
            if (deleterecord == null)
            {
                return NotFound();
            }
            _context.Employees.Remove(deleterecord);
            _context.SaveChanges();
            TempData["ResultOk"] = "Data Deleted Successfully !";
            return RedirectToAction("Index");
        }


    }
}