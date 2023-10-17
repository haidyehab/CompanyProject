using Comapny.DAL.Context;
using Comapny.DAL.Models;
using Company.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository :GenericRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(CompanyMvcDbContext dbContext):base(dbContext) 
        {
            
        }
    }
}
