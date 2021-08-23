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
    //Authorize project task controller.
    [Authorize]
    public class ProjectTasksController : Controller
    {
        private readonly Project_Management_SystemContext _context;

        public ProjectTasksController(Project_Management_SystemContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        //Gets all the project  tasks using a lamda query.
        public IActionResult Index()
        {
            var tasks = _context.Task.Include(p => p.Employee).Include(p => p.Project);
            return View(tasks.ToList());
        }

        // GET: ProjectTasks/Details/5
        //Gets project task details using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask =  _context.Task
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefault(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // GET: ProjectTasks/Create
        //Sends details to create task view.
        public IActionResult Create()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "UserName");
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectTitle");
            return View();
        }

        // POST: ProjectTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds a task to database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,TaskTitle,EstimatedNumberOfDays,ActualNumberOfDays,ProjectId,EmployeeId")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Id", projectTask.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "Id", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Edit/5
        //Get task for edit . Uses a linq quey to fetch task details.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask = (from task in _context.Task
                               where task.Id == id select task ).FirstOrDefault();
            if (projectTask == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "UserName", projectTask.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectTitle", projectTask.ProjectId);
            return View(projectTask);
        }

        // POST: ProjectTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates a task.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,TaskTitle,EstimatedNumberOfDays,ActualNumberOfDays,ProjectId,EmployeeId")] ProjectTask projectTask)
        {
            if (id != projectTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.Id))
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "UserName", projectTask.EmployeeId);
            ViewData["ProjectId"] = new SelectList(_context.Project, "Id", "ProjectTitle", projectTask.ProjectId);
            return View(projectTask);
        }

        // GET: ProjectTasks/Delete/5
        //Gets a task for deletion uses a lamda query to fetch task.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectTask =  _context.Task
                .Include(p => p.Employee)
                .Include(p => p.Project)
                .FirstOrDefault(m => m.Id == id);
            if (projectTask == null)
            {
                return NotFound();
            }

            return View(projectTask);
        }

        // POST: ProjectTasks/Delete/5
        //Deletes the task. Uses a linq query to find the task.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public  IActionResult DeleteConfirmed(string id)
        {
            var projectTask = (from task in _context.Task
                               where task.Id == id
                               select task).FirstOrDefault();
            _context.Task.Remove(projectTask);
             _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Uses a lamda query to check existance.
        private bool ProjectTaskExists(string id)
        {
            return _context.Task.Any(e => e.Id == id);
        }


        //Checks uniqueness using lamda. The validation check is done only for creates
        public bool TaskTitleAvailable(string taskTitle, string Id) {

            return !_context.Task.Any(e => e.TaskTitle.Equals(taskTitle) && Id ==null);
        }
    }
}
