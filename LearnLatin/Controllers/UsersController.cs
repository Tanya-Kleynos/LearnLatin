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

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
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
        public async Task<IActionResult> EditProfile(EditViewModel model)
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
    }
}
