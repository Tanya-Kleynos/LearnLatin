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
using LearnLatin.Models.ViewModels;

namespace LearnLatin.Controllers
{
    public class InputTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public InputTasksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: InputTasks
        public async Task<IActionResult> Index()
        {
            return View(await _context.InputTasks.ToListAsync());
        }

        // GET: InputTasks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTask = await _context.InputTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputTask == null)
            {
                return NotFound();
            }

            return View(inputTask);
        }

        public async Task<IActionResult> Display(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await _context.InputTasks
                .Include(t => t.Test)
                .Include(t => t.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (task == null)
            {
                return NotFound();
            }

            var taskView = new InputTaskViewModel
            {
                Id = task.Id,
                TestId = task.Test.Id,
                Description = task.Description
            };

            return View(taskView);
        }
        // GET: InputTasks/Create
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

        // POST: InputTasks/Create
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
                var task = new InputTask
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

        // GET: InputTasks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTask = await _context.InputTasks.FindAsync(id);
            if (inputTask == null)
            {
                return NotFound();
            }
            return View(inputTask);
        }

        // POST: InputTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Description,Created,Modified,NumInQueue")] InputTask inputTask)
        {
            if (id != inputTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inputTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InputTaskExists(inputTask.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(inputTask);
        }

        // GET: InputTasks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputTask = await _context.InputTasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputTask == null)
            {
                return NotFound();
            }

            return View(inputTask);
        }

        // POST: InputTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inputTask = await _context.InputTasks.FindAsync(id);
            _context.InputTasks.Remove(inputTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputTaskExists(Guid id)
        {
            return _context.InputTasks.Any(e => e.Id == id);
        }
    }
}
