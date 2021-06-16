using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LearnLatin.Models;
using LearnLatin.Models.ManageViewModels;

namespace LearnLatin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UsersController(UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        // GET: /Users/Edit
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.GetUserAsync(this.HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            EditViewModel model = new EditViewModel 
            { 
                UserName = user.UserName,
                Email = user.Email
            };
            return View(model);
        }

        // POST: /Users/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var user = await userManager.GetUserAsync(this.HttpContext.User);
            if (user != null)
            {
                user.UserName = model.UserName;
                user.Email = model.Email;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    //await this.signInManager.SignOutAsync();
                    //await this.signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, true, lockoutOnFailure: false);
                    return RedirectToAction("Index", "PersonalArea");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(model);
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm()
        {
            var user = await userManager.GetUserAsync(this.HttpContext.User);
            if (user != null)
            {
                await this.signInManager.SignOutAsync();
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
