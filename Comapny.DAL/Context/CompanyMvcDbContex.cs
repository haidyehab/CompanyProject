using Comapny.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comapny.DAL.Context
{
    public class CompanyMvcDbContext :IdentityDbContext<ApplicationUser>
    {
        public CompanyMvcDbContext(DbContextOptions<CompanyMvcDbContext> options):base(options)
        {
            
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
