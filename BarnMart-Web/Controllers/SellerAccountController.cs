using BarnMart_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using The_Barn_Mart;

namespace BarnMart_Web.Controllers
{
    //ONLY AVAILABLE FOR ADMIN
    [Authorize(Roles = "Admin")]
    public class SellerAccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SellerAccountController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.signInManager = signInManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public IActionResult SellerRegister()
        {

            return View(new SellerRegisterVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellerRegister(SellerRegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Lastname = model.Lastname,
                    Firstname = model.Firstname
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign the role "Buyer" to the user
                    await userManager.AddToRoleAsync(user, "Seller");

                    return RedirectToAction("LoginSuccess", "Home");
                }
                else { return View(model); }

                //var user = new ApplicationUser { UserName = model.Email, Email = model.Email };



                //user.UserName = model.Email;
                //user.Email = model.Email;
                //user.Lastname = model.Lastname;
                //user.Firstname = model.Firstname;


                //await userManager.CreateAsync(user, model.Password);
                //return RedirectToAction("LoginSuccess", "Home");
            }
            else { return View(model); }

        }


        public IActionResult SellerSignIn(string? returnUrl)
        {
            SignInVM vm = new SignInVM();
           
            if (!string.IsNullOrEmpty(returnUrl))
                vm.returnUrl = returnUrl;
            return View(vm);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SellerSignIn(SignInVM model, string? returnUrl)
        {
            //Codes to Sign in 
            IdentityUser user = await userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    httpContextAccessor.HttpContext.Session.SetString("UserId", user.Id);
                    httpContextAccessor.HttpContext.Session.SetString("UserRole", "Seller");
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    else 
                    {
                        return RedirectToAction("LoginSuccess", "Home");
                    }
                        
                }
                else
                {
                    ModelState.AddModelError("Login Error", "Invalid Credentials");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Login Error", "Invalid Credentials");
                return View(model);
            }


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            httpContextAccessor.HttpContext.Session.Clear();
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
