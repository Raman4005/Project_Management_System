using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_Management_System.Models;

[assembly: HostingStartup(typeof(Project_Management_System.Areas.Identity.IdentityHostingStartup))]
namespace Project_Management_System.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<Project_Management_SystemIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("Project_Management_SystemIdentityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<Project_Management_SystemIdentityContext>();
            });
        }
    }
}