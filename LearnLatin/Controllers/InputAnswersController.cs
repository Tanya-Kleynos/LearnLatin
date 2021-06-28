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

namespace LearnLatin.Controllers
{
    public class InputAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public InputAnswersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: InputAnswers
        public async Task<IActionResult> Index(Guid taskId)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var answers = await this._context.InputAnswers
                .Where(a => a.Task.Id == taskId)
                .ToListAsync();

            var task = await this._context.InputTasks
                .Include(t => t.Test)
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (answers == null)
            {
                return this.NotFound();
            }
            this.ViewBag.TaskId = taskId;
            this.ViewBag.Test = task.Test;
            return View(answers);
        }

        // GET: InputAnswers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputAnswer = await _context.InputAnswers
                .Include(a => a.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputAnswer == null)
            {
                return NotFound();
            }
            ViewBag.Task = inputAnswer.Task;
            return View(inputAnswer);
        }

        // GET: InputAnswers/Create
        public async Task<IActionResult> Create(Guid taskId)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var task = await this._context.InputTasks
                .Include(t => t.Test)
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                return this.NotFound();
            }

            ViewBag.Test = task.Test;
            ViewBag.Task = task;
            return this.View(new InputAnswerCreateViewModel());
        }

        // POST: InputAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid taskId, InputAnswerCreateViewModel model)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var task = await this._context.InputTasks
                .Include(t => t.Test)
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                return this.NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var ans = new InputAnswer
                {
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Creator = user,
                    Editor = user,
                    Task = task,
                    AnsValue = model.AnsValue
                };

                this._context.Add(ans);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Tests", new { id = task.Test.Id });
            }
            this.ViewBag.Task = task;
            return View(model);
        }

        // GET: InputAnswers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputAnswer = await _context.InputAnswers
                .Include(a => a.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputAnswer == null)
            {
                return NotFound();
            }
            ViewBag.Task = inputAnswer.Task;
            var model = new InputAnswerCreateViewModel
            {
                AnsValue = inputAnswer.AnsValue
            };
            return View(model);
        }

        // POST: InputAnswers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, InputAnswerCreateViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ans = await _context.InputAnswers
                .Include(a => a.Task)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ans == null)
            {
                return NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (ModelState.IsValid)
            {
                ans.AnsValue = model.AnsValue;
                ans.Modified = DateTime.Now;
                ans.Editor = user;

                await this._context.SaveChangesAsync();
                return RedirectToAction("Index", "InputAnswers", new { taskId = ans.Task.Id });
            }
            return View(model);
        }

        // GET: InputAnswers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inputAnswer = await _context.InputAnswers
                .Include(a => a.Task)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inputAnswer == null)
            {
                return NotFound();
            }
            ViewBag.Task = inputAnswer.Task;
            return View(inputAnswer);
        }

        // POST: InputAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var inputAnswer = await _context.InputAnswers.FindAsync(id);
            _context.InputAnswers.Remove(inputAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InputAnswerExists(Guid id)
        {
            return _context.InputAnswers.Any(e => e.Id == id);
        }
    }
}
