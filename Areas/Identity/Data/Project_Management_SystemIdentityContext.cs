using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project_Management_System.Models
{
    //Responsible for mapping identity related built in classes to the ASP NET identity tables 
    public class Project_Management_SystemIdentityContext : IdentityDbContext<IdentityUser>
    {
        public Project_Management_SystemIdentityContext(DbContextOptions<Project_Management_SystemIdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
