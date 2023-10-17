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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyMvcDbContext _dbContext;

        public EmployeeRepository(CompanyMvcDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }
        public IQueryable<Employee> GetEmployeeByAddress(string address)
       => _dbContext.Employees.Where(E =>E.Address == address);

        public IQueryable<Employee> SearchEmployeesByName(string Name)
        
         =>  _dbContext.Employees.Where(E => E.Name.ToLower().Contains(Name.ToLower()));
        
    }
}
