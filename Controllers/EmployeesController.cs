using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    //Authorized employee controller
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly Project_Management_SystemContext _context;

        public EmployeesController(Project_Management_SystemContext context)
        {
            _context = context;
        }

        // GET: Employees
        //Gets all employees using a linq query.
        public IActionResult Index()
        {
            return View((from employee in _context.Employee select employee).ToList());
        }

        // GET: Employees/Details/5
        //Gets an employee using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  _context.Employee
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        //Provides model data to employee create view.
        public IActionResult Create()
        {

            Position[] positions =  new Position[] { Position.Engineer, Position.Manager };

            ViewData["Position"] = new SelectList(positions);
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds a new employee to database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,UserName,Position")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                 _context.Add(employee);
                 _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Edit/5
        //Gets  an employee for update.
        //Checks existance using a linq query.
        public  IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = (from findEmployee in _context.Employee
                           where findEmployee.Id == id select findEmployee).FirstOrDefault();
            if (employee == null)
            {
                return NotFound();
            }

            Position[] positions = new Position[] { Position.Engineer, Position.Manager };
            ViewData["Position"] = new SelectList(positions, employee.Position);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates an employee.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,UserName,Position")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        //Gets an employee for delete .Check for existance using lamda query.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee =  _context.Employee
                .FirstOrDefault(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        //Deletes an employee. uses a linq query to check existance.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var employee = (from findEmployee in _context.Employee
                            where findEmployee.Id == id
                            select findEmployee).FirstOrDefault();
            _context.Employee.Remove(employee);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Uses a lamda query to check existance.
        private bool EmployeeExists(string id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }


        //Checks whether the username is available.The validation check is done only for creates
        public bool UserNameAvailable(string UserName, string Id) {

            return !_context.Employee.Any(e => e.UserName.Equals(UserName) && Id == null);
        }
    }


}
