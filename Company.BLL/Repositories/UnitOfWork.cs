using Comapny.DAL.Context;
using Company.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompanyMvcDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get ; set ; }
        public IDepartmentRepository DepartmentRepository { get ; set ;}

        public UnitOfWork(CompanyMvcDbContext dbContext)//clr
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public async Task<int> complete()
         =>await _dbContext.SaveChangesAsync();

        public void Dispose() 
       => _dbContext.Dispose();
    }
}
