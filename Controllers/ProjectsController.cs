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
    //Authorized Project controller.
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly Project_Management_SystemContext _context;

        public ProjectsController(Project_Management_SystemContext context)
        {
            _context = context;
        }

        // GET: Projects
        //Returns a project list using a linq query.
        public IActionResult Index()
        {
            return View( (from project in _context.Project select project).ToList());
        }

        // GET: Projects/Details/5
        //Gets the project details using a lamda query.
        public IActionResult Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project =  _context.Project
                .FirstOrDefault(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        //Returns the create project view.
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Adds  a project to database.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,ProjectTitle,Budget,CompletionDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        //Gets a project to edit. Gets the project using a linq query.
        public IActionResult Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project =  (from projectSearch in _context.Project where
                            projectSearch.Id == id select projectSearch).FirstOrDefault();
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //Updates a project
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, [Bind("Id,ProjectTitle,Budget,CompletionDate")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            return View(project);
        }

        // GET: Projects/Delete/5
        //Gets a projects for deletion . Check the existance with a linq query.
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = _context.Project
                .FirstOrDefault(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        //Deletes a project
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var project = (from projectDelete in _context.Project
                           where projectDelete.Id == id select projectDelete).FirstOrDefault();
            _context.Project.Remove(project);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        //Check existence uses a lamda query.
        private bool ProjectExists(string id)
        {
            return _context.Project.Any(e => e.Id == id);
        }


        //Uniqueness check using lamda . The validation check is done only for creates
        public bool ProjectTitleAvailable(string ProjectTitle, string Id) {

            return !_context.Project.Any(e => e.ProjectTitle.Equals(ProjectTitle) && Id == null);
        }
    }
}
