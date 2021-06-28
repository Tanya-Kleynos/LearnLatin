using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnLatin.Data;
using LearnLatin.Models;
using LearnLatin.Models.CreateViewModels;
using Microsoft.AspNetCore.Identity;
using LearnLatin.Models.EditViewModels;
using LearnLatin.Models.ViewModels;

namespace LearnLatin.Controllers
{
    public class TrueOutOfFalseTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TrueOutOfFalseTasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TrueOutOfFalseTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrueOutOfFalseTasks.ToListAsync());
        }

        // GET: TrueOutOfFalseTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trueOutOfFalseTask = await _context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .Include(t => t.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trueOutOfFalseTask == null)
            {
                return NotFound();
            }
            ViewBag.Test = trueOutOfFalseTask.Test; 
            return View(trueOutOfFalseTask);
        }

        public async Task<IActionResult> Display(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .Include(t => t.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            var taskView = new TaskViewModel
            {
                Id = task.Id,
                TestId = task.Test.Id,
                Description = task.Description,
                Answers = task.Answers
            };

            return View(taskView);
        }

        // GET: TrueOutOfFalseTasks/Create
        public async Task<IActionResult> Create(Guid testId)
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
            return this.View(new TaskCreateViewModel());
        }

        // POST: TrueOutOfFalseTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid testId, TaskCreateViewModel model)
        {
            if (testId == null)
            {
                return this.NotFound();
            }

            var test = await this._context.Tests
                .Include(t => t.Tasks)
                .SingleOrDefaultAsync(x => x.Id == testId);

            if (test == null)
            {
                return this.NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var task = new TrueOutOfFalseTask
                {
                    Test = test,
                    Description = model.Description,
                    Created = DateTime.Now,
                    Creator = user
                };

                if (test.NumOfTasks == null)
                {
                    task.NumInQueue = 1;
                    test.NumOfTasks = 1;
                }
                else
                {
                    task.NumInQueue = (Int32)test.NumOfTasks + 1;
                    test.NumOfTasks++;
                }
                this._context.Add(task);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Tests", new { id = task.Test.Id });
            }
            this.ViewBag.Test = test;
            return View(model);
        }

        // GET: TrueOutOfFalseTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await this._context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .Include(t => t.Creator)
                .SingleOrDefaultAsync(x => x.Id == id);

            if (task == null)
            {
                return NotFound();
            }
            var model = new TaskEditViewModel
            {
                Description = task.Description
            };
            ViewBag.Test = task.Test;
            return View(task);
        }

        // POST: TrueOutOfFalseTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid testId, TaskEditViewModel model)
        {
            if (testId == null)
            {
                return NotFound();
            }

            var task = await this._context.TrueOutOfFalseTasks
                .Include(x => x.Test)
                .SingleOrDefaultAsync(x => x.Id == testId);

            if (task == null)
            {
                return NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (ModelState.IsValid)
            {

                task.Description = model.Description;
                task.Modified = DateTime.Now;
                task.Editor = user;

                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Tests", new { id = task.Test.Id });
            }
            this.ViewBag.Test = task.Test;
            return View(task);
        }

        // GET: TrueOutOfFalseTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trueOutOfFalseTask = await _context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .Include(t => t.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trueOutOfFalseTask == null)
            {
                return NotFound();
            }
            ViewBag.Test = trueOutOfFalseTask.Test;
            return View(trueOutOfFalseTask);
        }

        // POST: TrueOutOfFalseTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trueOutOfFalseTask = await _context.TrueOutOfFalseTasks.FindAsync(id);
            _context.TrueOutOfFalseTasks.Remove(trueOutOfFalseTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrueOutOfFalseTaskExists(Guid id)
        {
            return _context.TrueOutOfFalseTasks.Any(e => e.Id == id);
        }

    }
}
