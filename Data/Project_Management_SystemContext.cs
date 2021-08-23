using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project_Management_System.Models;

namespace Project_Management_System.Models
{
    //Responsible for connecting mapping and connecting model classes to the underlying database.
    public class Project_Management_SystemContext : DbContext
    {
        public Project_Management_SystemContext (DbContextOptions<Project_Management_SystemContext> options)
            : base(options)
        {
        }

        public DbSet<Project_Management_System.Models.Project> Project { get; set; }

        public DbSet<Project_Management_System.Models.Employee> Employee { get; set; }

        public DbSet<Project_Management_System.Models.ProjectTask> Task { get; set; }
    }
}
