using AutoMapper;
using Comapny.DAL.Models;
using Company.PL.Models;

namespace Company.PL.MappedProfiles
{
    public class EmployeeProfile :Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
