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

namespace LearnLatin.Controllers
{
    public class TrueOutOfFalseAnswersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TrueOutOfFalseAnswersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TrueOutOfFalseAnswers
        public async Task<IActionResult> Index(Guid taskId)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var answers = await this._context.TrueOutOfFalseAnswers
                .Where(a => a.Task.Id == taskId)
                .ToListAsync();

            if (answers == null)
            {
                return this.NotFound();
            }

            return View(answers);
        }

        // GET: TrueOutOfFalseAnswers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trueOutOfFalseAnswer = await _context.TrueOutOfFalseAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trueOutOfFalseAnswer == null)
            {
                return NotFound();
            }

            return View(trueOutOfFalseAnswer);
        }

        // GET: TrueOutOfFalseAnswers/Create
        public async Task<IActionResult> Create(Guid taskId)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var task = await this._context.TrueOutOfFalseTasks
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                return this.NotFound();
            }

            this.ViewBag.Task = task;
            return this.View(new TrueOutOfFalseAnswerCreateViewModel());
        }

        // POST: TrueOutOfFalseAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid taskId, TrueOutOfFalseAnswerCreateViewModel model)
        {
            if (taskId == null)
            {
                return this.NotFound();
            }

            var task = await this._context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .SingleOrDefaultAsync(x => x.Id == taskId);

            if (task == null)
            {
                return this.NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var ans = new TrueOutOfFalseAnswer
                {
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    Creator = user,
                    Editor = user,
                    Task = task,
                    AnsValue = model.AnsValue,
                    IsTrue = model.IsTrue
                };

                this._context.Add(ans);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Tests", new { id = task.Test.Id });
            }
            this.ViewBag.Task = task;
            return View(model);
        }

        // GET: TrueOutOfFalseAnswers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trueOutOfFalseAnswer = await _context.TrueOutOfFalseAnswers.FindAsync(id);
            if (trueOutOfFalseAnswer == null)
            {
                return NotFound();
            }
            return View(trueOutOfFalseAnswer);
        }

        // POST: TrueOutOfFalseAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Created,Modified,AnsValue,IsTrue")] TrueOutOfFalseAnswer trueOutOfFalseAnswer)
        {
            if (id != trueOutOfFalseAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trueOutOfFalseAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrueOutOfFalseAnswerExists(trueOutOfFalseAnswer.Id))
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
            return View(trueOutOfFalseAnswer);
        }

        // GET: TrueOutOfFalseAnswers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trueOutOfFalseAnswer = await _context.TrueOutOfFalseAnswers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trueOutOfFalseAnswer == null)
            {
                return NotFound();
            }

            return View(trueOutOfFalseAnswer);
        }

        // POST: TrueOutOfFalseAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var trueOutOfFalseAnswer = await _context.TrueOutOfFalseAnswers.FindAsync(id);
            _context.TrueOutOfFalseAnswers.Remove(trueOutOfFalseAnswer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrueOutOfFalseAnswerExists(Guid id)
        {
            return _context.TrueOutOfFalseAnswers.Any(e => e.Id == id);
        }
    }
}
