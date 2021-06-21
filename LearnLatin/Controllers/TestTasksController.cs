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
using LearnLatin.Models.CreateViewModels;
using LearnLatin.Models.EditViewModels;

namespace LearnLatin.Controllers
{
    public class TestTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TestTasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TestTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TestTasks.ToListAsync());
        }

        // GET: TestTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTask = await _context.TestTasks
                .Include(t => t.Test)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (testTask == null)
            {
                return NotFound();
            }

            return View(testTask);
        }

        // GET: TestTasks/Create
        public async Task<IActionResult> CreateAsync(Guid testId)
        {
            if (testId == null)
            {
                return this.NotFound();
            }

            var test = await this._context.Tests
                .SingleOrDefaultAsync(x => x.Id == testId);

            if (test == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Test = test;
            return this.View(new TestTaskCreateViewModel());
        }

        // POST: TestTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid testId, TestTaskCreateViewModel model)
        {
            if (testId == null)
            {
                return this.NotFound();
            }

            var test = await this._context.Tests
                .SingleOrDefaultAsync(x => x.Id == testId);

            if (test == null)
            {
                return this.NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var testTask = new TestTask
                {
                    Description = model.Description,
                    Created = DateTime.Now,
                    Creator = user
                };

                this._context.Add(testTask);
                await this._context.SaveChangesAsync();
                /*return this.RedirectToAction("Index", "ForumCategories", new { forumCategoryId = forumCategory.Id });*/
            }
            this.ViewBag.Test = test;
            return View(model);
        }

        // GET: TestTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? testId)
        {
            if (testId == null)
            {
                return NotFound();
            }

            var testTask = await _context.TestTasks.FindAsync(testId);
            if (testTask == null)
            {
                return NotFound();
            }
            var model = new TestTaskEditViewModel
            {
                Description = testTask.Description
            };
            return View(testTask);
        }

        // POST: TestTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid testId, TestTaskEditViewModel model)
        {
            if (testId == null)
            {
                return NotFound();
            }

            var testTask = await _context.TestTasks.FindAsync(testId);

            if (testTask == null)
            {
                return NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (ModelState.IsValid)
            {

                testTask.Description = model.Description;
                testTask.Modified = DateTime.Now;
                testTask.Editor = user;

                await this._context.SaveChangesAsync();
                /*return this.RedirectToAction("Index", "ForumCategories", new { forumCategoryId = forumCategory.Id });*/
            }
            this.ViewBag.Test = testTask.Test;
            return View(testTask);
        }

        // GET: TestTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testTask = await _context.TestTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testTask == null)
            {
                return NotFound();
            }

            return View(testTask);
        }

        // POST: TestTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var testTask = await _context.TestTasks.FindAsync(id);
            _context.TestTasks.Remove(testTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestTaskExists(Guid id)
        {
            return _context.TestTasks.Any(e => e.Id == id);
        }
    }
}
