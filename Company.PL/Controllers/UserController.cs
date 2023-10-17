using AutoMapper;
using Comapny.DAL.Models;
using Company.PL.Helpers;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager,
			IMapper mapper)
        {
			_userManager = userManager;
			_signInManager = signInManager;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
		{
			if(string.IsNullOrEmpty(SearchValue))
			{
				var users =await _userManager.Users.Select(U => new UserViewModel()
				{
					Id = U.Id,
					Fname = U.Fname,
					Lname = U.Lname,
					Email = U.Email,
					PhoneNumber = U.PhoneNumber,
					Roles = _userManager.GetRolesAsync(U).Result
				}).ToListAsync(); ;
				return View(users);
			}
			else
			{
				var user=await _userManager.FindByEmailAsync(SearchValue);
                if(user is not null)
                {
                    var MappedUser = new UserViewModel()
                    {
                        Id = user.Id,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        Roles = _userManager.GetRolesAsync(user).Result
                    };
                    return View(new List<UserViewModel> { MappedUser });
                }
                return View(Enumerable.Empty<UserViewModel>());
				
			}
          }

    public async Task<IActionResult> Details(string Id , string ViewName = "Details")
	{
			var user =await _userManager.FindByIdAsync(Id);
			var mappedUser = _mapper.Map<ApplicationUser, UserViewModel>(user);
			return View(ViewName, mappedUser);
	}

        public async Task<IActionResult> Edit(string id)
        {

            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, UserViewModel UpdatedUser)
        {
            if (id != UpdatedUser.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    user.Fname = UpdatedUser.Fname;
                    user.Lname = UpdatedUser.Lname;
                    user.PhoneNumber = UpdatedUser.PhoneNumber;
                   
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction(nameof(Index));

                }
                catch (Exception ex)
                {

                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(UpdatedUser);
        }

  //Delete
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, UserViewModel DeletdUser)
        {
            if (id != DeletdUser.Id)
                return BadRequest();
            try
            {
                var user = await _userManager.FindByIdAsync(id);
              
                await   _userManager.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(DeletdUser);
            }
        }





    }
}
