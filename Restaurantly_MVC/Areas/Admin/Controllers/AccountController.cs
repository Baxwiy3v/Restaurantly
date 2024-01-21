using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Restaurantly_MVC.Areas.Admin.ViewModels;
using Restaurantly_MVC.Models;
using Restaurantly_MVC.Utitilies.Enum;

namespace Restaurantly_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _manager;
        private readonly SignInManager<AppUser> _sign;
        private readonly RoleManager<IdentityRole> _role;

        public AccountController(UserManager<AppUser> manager, SignInManager<AppUser> sign, RoleManager<IdentityRole> role)
        {
            _manager = manager;
            _sign = sign;
            _role = role;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            registerVM.Name = registerVM.Name.Trim();
            registerVM.Surname = registerVM.Surname.Trim();

            string name = Char.ToUpper(registerVM.Name[0]) + registerVM.Name.Substring(1);
            string surname = Char.ToUpper(registerVM.Surname[0]) + registerVM.Surname.Substring(1);


            AppUser user = new AppUser
            {


                Name = name,
                Surname = surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName

            };

            var manager = await _manager.CreateAsync(user, registerVM.Password);

            if (!manager.Succeeded)
            {
                foreach (var error in manager.Errors)
                {
                    ModelState.AddModelError(String.Empty, error.Description);

                }

                return View(registerVM);


            }

            await _sign.SignInAsync(user, isPersistent: false);



            return RedirectToAction("Index", "Home", new { Area = " " });
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _manager.FindByNameAsync(loginVM.UserOrEmail);

            if (user == null)
            {
                user = await _manager.FindByEmailAsync(loginVM.UserOrEmail);

                if (user == null)
                {
                    ModelState.AddModelError(String.Empty, "User,Email ve ya Password uygun deyil");
                    return View(loginVM);

                }


            }


            var sigin = await _sign.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, true);

            if (sigin.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty, "Heddinden artiq cehd biraz sonra yeniden yoxlayin");
                return View(loginVM);
            }

            if (!sigin.Succeeded)
            {

                ModelState.AddModelError(String.Empty, "User,Email ve ya Password uygun deyil");
                return View(loginVM);
            }



            return RedirectToAction("Index", "Home", new { Area = " " });
        }

        public async Task<IActionResult> CreateRoles()
        {

            foreach (var role in Enum.GetValues(typeof(UserRole)))
            {
                await _role.CreateAsync(new IdentityRole
                {
                    Name = role.ToString()

                });

            }
            return RedirectToAction("Index", "Home", new { Area = " " });
        }

        public async Task<IActionResult> Logout()
        {
            await _sign.SignOutAsync();
          
            return RedirectToAction("Index", "Home", new { Area = " " });
        }



    }
}

