using Comapny.DAL.Models;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser>  userManager,
            SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
            if(ModelState.IsValid)//server side validation for backend
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree,
                    Fname = model.Fname,
                    Lname = model.Lname,
                };
                var Result=await _userManager.CreateAsync(user,model.Password);
                if(Result.Succeeded)
                
                    return RedirectToAction(nameof(Login));
                    foreach(var error in Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);

					}
             }
			return View(model);
		}
    public IActionResult Login()
    {
            return View();
    }
      [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
		{
            if(ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                  var Flag= await _userManager.CheckPasswordAsync(user, model.Password);
                    if(Flag)
                    {
                     var Result=  await  _signInManager.PasswordSignInAsync(user,model.Password,model.RememberMe ,false);
                        if (Result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
					ModelState.AddModelError(string.Empty, "Incorrect Password");
				}
                ModelState.AddModelError(string.Empty,"Email is not Exsits");
            }
			return View(model);
		}
  //SignOut
    public new async Task<IActionResult> SignOut()
    {
           await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
    }


	}
}
