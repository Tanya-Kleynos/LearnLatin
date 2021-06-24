using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnLatin.Data;
using LearnLatin.Models;
using Microsoft.AspNetCore.Identity;

namespace LearnLatin.Controllers
{
    public class UserTestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserTestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserTests/Create

        public async Task<IActionResult> Create(Guid testId)
        {
            var test = await _context.Tests
                .SingleOrDefaultAsync(x => x.Id == testId);

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (test != null)
            {
                if (ModelState.IsValid)
                {
                    var userTest = new UserTest
                    {
                        User = user,
                        Test = test,
                    };
                    if (test.NumOfRightAnswers == null)
                    {
                        userTest.LastResult = 0;
                        userTest.BestResult = 0;
                    }
                    else
                    {
                        userTest.LastResult = (Int32)test.NumOfRightAnswers;
                        userTest.BestResult = (Int32)test.NumOfRightAnswers;
                    }
                    _context.Add(userTest);
                    test.IsNotForTheFirstTime = true;
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index", "PersonalArea");
        }

        // GET: UserTests/Edit/5
        public async Task<IActionResult> Edit(Guid testId)
        {
            var test = await _context.Tests
                .Include(t => t.Tasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.InputTasks)
                .ThenInclude(t => t.Answers)
                .SingleOrDefaultAsync(x => x.Id == testId);

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            var userTest = await _context.UserTests
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Test.Id == testId)
                .SingleOrDefaultAsync();
                //.SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (test != null)
            {
                if (ModelState.IsValid)
                {
                    userTest.LastResult = test.NumOfRightAnswers;
                    if (userTest.LastResult > userTest.BestResult)
                    {
                        userTest.BestResult = userTest.LastResult;
                    }
                    await _context.SaveChangesAsync();
                }
            }

            return View("~/Views/Tests/Results.cshtml", test);
        }

        // GET: UserTests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTest = await _context.UserTests
                .Include(u => u.Test)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userTest == null)
            {
                return NotFound();
            }

            return View(userTest);
        }

        // POST: UserTests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userTest = await _context.UserTests.FindAsync(id);
            _context.UserTests.Remove(userTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserTestExists(Guid id)
        {
            return _context.UserTests.Any(e => e.UserId == id);
        }
    }
}
