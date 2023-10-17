using AutoMapper;
using Comapny.DAL.Models;
using Company.PL.Models;

namespace Company.PL.MappedProfiles
{
    public class UserProfile :Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
