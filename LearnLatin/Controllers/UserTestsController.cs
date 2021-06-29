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
            var curTest = await _context.Tests
                .Include(t => t.Tasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.InputTasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(x => x.Id == testId);

            var tests = await _context.Tests
                .Where(t => t.Theme.Id == curTest.Theme.Id)
                .ToListAsync();

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            var userTheme = await _context.UserThemes
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Theme.Id == curTest.Theme.Id)
                .SingleOrDefaultAsync();

            var allTasksCount = 0;
            var rightTasksCount = 0;

            foreach (var item in tests)
            {
                var userTest = await _context.UserTests
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Test.Id == item.Id)
                .SingleOrDefaultAsync();

                allTasksCount += (int)item.NumOfTasks;
                if (userTest != null)
                {
                    rightTasksCount += (int)userTest.BestResult;
                }
                else
                {
                    rightTasksCount += (int)item.NumOfRightAnswers;
                }
            }
            if (userTheme == null)
            {
                var usrTheme = new UserTheme
                {
                    User = user,
                    Theme = curTest.Theme,
                    Progress = (int?)Math.Round(((double)rightTasksCount / allTasksCount) * 100, 0)
                };
                _context.Add(usrTheme);
            }
            else
            {
                userTheme.Progress = (int?)Math.Round(((double)rightTasksCount / allTasksCount) * 100, 0);
            }
            await _context.SaveChangesAsync();

            if (curTest != null)
            {
                if (ModelState.IsValid)
                {
                    var userTest = new UserTest
                    {
                        User = user,
                        Test = curTest,
                    };
                    if (curTest.NumOfRightAnswers == null)
                    {
                        userTest.LastResult = 0;
                        userTest.BestResult = 0;
                    }
                    else
                    {
                        userTest.LastResult = (Int32)curTest.NumOfRightAnswers;
                        userTest.BestResult = (Int32)curTest.NumOfRightAnswers;
                    }
                    _context.Add(userTest);
                    curTest.IsNotForTheFirstTime = true;
                    await _context.SaveChangesAsync();
                }
            }

            return View("~/Views/Tests/Results.cshtml", curTest);
        }

        // GET: UserTests/Edit/5
        public async Task<IActionResult> Edit(Guid testId)
        {
            var curTest = await _context.Tests
                .Include(t => t.Tasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.InputTasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.Theme)
                .SingleOrDefaultAsync(x => x.Id == testId);

            var tests = await _context.Tests
                .Where(t => t.Theme.Id == curTest.Theme.Id)
                .ToListAsync();

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            var userTest = await _context.UserTests
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Test.Id == testId)
                .SingleOrDefaultAsync();
            var userTheme = await _context.UserThemes
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Theme.Id == curTest.Theme.Id)
                .SingleOrDefaultAsync();
            //.SingleOrDefaultAsync(x => x.User.Id == user.Id);

            if (curTest != null)
            {
                if (ModelState.IsValid)
                {
                    userTest.LastResult = curTest.NumOfRightAnswers;
                    if (userTest.LastResult > userTest.BestResult)
                    {
                        userTest.BestResult = userTest.LastResult;
                    }
                    await _context.SaveChangesAsync();
                }
            }

            var allTasksCount = 0;
            var rightTasksCount = 0;

            foreach (var item in tests)
            {
                var usrTest = await _context.UserTests
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Test.Id == item.Id)
                .SingleOrDefaultAsync();

                allTasksCount += (int)item.NumOfTasks;
                if (usrTest != null)
                {
                    rightTasksCount += (int)usrTest.BestResult;
                }
                else
                {
                    rightTasksCount += (int)item.NumOfRightAnswers;
                }
            }

            if (userTheme == null)
            {
                var usrTheme = new UserTheme
                {
                    User = user,
                    Theme = curTest.Theme,
                    Progress = (int?)Math.Round(((double)rightTasksCount / allTasksCount) * 100, 0)
                };
                _context.Add(usrTheme);
            }
            else
            {
                userTheme.Progress = (int?)Math.Round(((double)rightTasksCount / allTasksCount) * 100, 0);
            }

            await _context.SaveChangesAsync();

            return View("~/Views/Tests/Results.cshtml", curTest);
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
