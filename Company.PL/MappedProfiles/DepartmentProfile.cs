using AutoMapper;
using Comapny.DAL.Models;
using Company.PL.Models;

namespace Company.PL.MappedProfiles
{
    public class DepartmentProfile :Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel , Department>().ReverseMap();
        }
    }
}
