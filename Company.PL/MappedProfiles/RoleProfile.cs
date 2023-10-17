using AutoMapper;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;

namespace Company.PL.MappedProfiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d => d.Name,O => O.MapFrom(S => S.RoleName)).ReverseMap();
        }
    }
}
