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
    public class TestsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TestsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tests
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tests
                .Include(t => t.Creator)
                .Include(t => t.Editor)
                .ToListAsync());
        }

        // GET: Tests/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Creator)
                .Include(t => t.Editor)
                .Include(t => t.Tasks)
                .ThenInclude(x => x.Answers)
                .Include(t => t.Tasks)
                .ThenInclude(x => x.Creator)
                .Include(t => t.InputTasks)
                .ThenInclude(x => x.Answers)
                .Include(t => t.InputTasks)
                .ThenInclude(x => x.Creator)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }
        public async Task<IActionResult> Start(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.Tasks)
                .ThenInclude(t => t.Answers)
                .Include(t => t.InputTasks)
                .ThenInclude(t => t.Answers)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (test == null || test.NumOfTasks == null)
            {
                return RedirectToAction("Index", "PersonalArea");
            }

            int taskCounter = 0;
            foreach (var item in test.Tasks)
            {
                if (item.Answers.Count > 1)
                {
                    taskCounter++;
                }
            }
            int inTaskCounter = 0;
            foreach (var item in test.InputTasks)
            {
                if (item.Answers.Count > 0)
                {
                    inTaskCounter++;
                }
            }

            if (taskCounter != test.Tasks.Count || inTaskCounter != test.InputTasks.Count)
            {
                return RedirectToAction("Index", "PersonalArea");
            }

            test.NumOfRightAnswers = null;
            foreach (var item in test.Tasks)
            {
                item.IsAnsweredRight = false;
            }
            foreach (var item in test.InputTasks)
            {
                item.IsAnsweredRight = false;
            }
            await this._context.SaveChangesAsync();

            foreach (var item in test.Tasks)
            {
                if (item.NumInQueue == 1)
                {
                    return RedirectToAction("Display", "TrueOutOfFalseTasks", new { id = item.Id });
                }
            }
            foreach (var item in test.InputTasks)
            {
                if (item.NumInQueue == 1)
                {
                    return RedirectToAction("Display", "InputTasks", new { id = item.Id });
                }
            }

            return View();
        }

        // GET: Tests/Create
        public IActionResult Create()
        {
            return View(new TestCreateViewModel());
        }

        // POST: Tests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TestCreateViewModel model)
        {
            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var test = new Test
                {
                    Name = model.Name,
                    Description = model.Description,
                    Created = DateTime.Now,
                    Creator = user,
                    Editor = user,
                    Modified = DateTime.Now,
                    NumOfTasks = 0
                };

                this._context.Add(test);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index", "PersonalArea");
            }
            return View(model);
        }

        // GET: Tests/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests.FindAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var model = new TestEditViewModel
            {
                Name = test.Name,
                Description = test.Description
            };
            return View(model);
        }

        // POST: Tests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, TestEditViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.Id == id);

            if (test == null)
            {
                return NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (ModelState.IsValid)
            {
                test.Name = model.Name;
                test.Description = model.Description;
                test.Modified = DateTime.Now;
                test.Editor = user;

                await this._context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Tests/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .FirstOrDefaultAsync(m => m.Id == id);
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Tests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var test = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestExists(Guid id)
        {
            return _context.Tests.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SaveResults(Guid? testId, Guid taskId, Guid userAnswerId, String userInputAnswer)
        {
            if (testId == null)
            {
                return NotFound();
            }

            var test = await _context.Tests
                .Include(t => t.InputTasks)
                .Include(t => t.Tasks)
                .FirstOrDefaultAsync(m => m.Id == testId);

            if (test == null)
            {
                return NotFound();
            }

            if (taskId == null)
            {
                return NotFound();
            }
            var trueOutOfFalseTask = await _context.TrueOutOfFalseTasks
                .Include(t => t.Test)
                .ThenInclude(t => t.Theme)
                .FirstOrDefaultAsync(m => m.Id == taskId);

            var inputTask = await _context.InputTasks
                .Include(i => i.Answers)
                .Include(t => t.Test)
                .ThenInclude(t => t.Theme)
                .FirstOrDefaultAsync(m => m.Id == taskId);



            if (trueOutOfFalseTask != null)
            {
                var userAnswer = await _context.TrueOutOfFalseAnswers
                    .FirstOrDefaultAsync(m => m.Id == userAnswerId);

                if (userAnswer.IsTrue && trueOutOfFalseTask.NumInQueue == 1)
                {
                    test.NumOfRightAnswers = 1;
                    trueOutOfFalseTask.IsAnsweredRight = true;
                }
                else if (userAnswer.IsTrue)
                {
                    test.NumOfRightAnswers++;
                    trueOutOfFalseTask.IsAnsweredRight = true;
                }

                await _context.SaveChangesAsync();

                if (trueOutOfFalseTask.NumInQueue == trueOutOfFalseTask.Test.NumOfTasks) // если таск последний в очереди
                {
                    // if for the first time
                    if (test.IsNotForTheFirstTime)
                    {
                        return RedirectToAction("Edit", "UserTests", new { testId = test.Id });
                    }
                    else
                    {
                        return RedirectToAction("Create", "UserTests", new { testId = test.Id });
                    }
                   
                    // return RedirectToAction("Edit", "UserTests", new { testId = test.Id });

                    //return RedirectToAction("Index", "PersonalArea"); // подумать куда идти после окончания теста
                }
                else // если таск не последний в очереди
                {

                    foreach (var item in test.Tasks) // ищем следующий по очереди таск
                    {
                        if (item.NumInQueue == (trueOutOfFalseTask.NumInQueue + 1))
                        {
                            return RedirectToAction("Display", "TrueOutOfFalseTasks", new { id = item.Id });
                        }
                    }
                    foreach (var item in test.InputTasks) // ищем следующий по очереди таск
                    {
                        if (item.NumInQueue == (trueOutOfFalseTask.NumInQueue + 1))
                        {
                            return RedirectToAction("Display", "InputTasks", new { id = item.Id });
                        }
                    }
                }
            }
            else if (inputTask != null)
            {
                foreach (var item in inputTask.Answers)
                {
                    if (item.AnsValue == userInputAnswer && inputTask.NumInQueue == 1)
                    {
                        test.NumOfRightAnswers = 1;
                        inputTask.IsAnsweredRight = true;
                        break;
                    }
                    else if (item.AnsValue == userInputAnswer)
                    {
                        test.NumOfRightAnswers++;
                        inputTask.IsAnsweredRight = true;
                        break;
                    }
                }
                await _context.SaveChangesAsync();

                if (inputTask.NumInQueue == inputTask.Test.NumOfTasks) // если таск последний в очереди
                {
                    if (test.IsNotForTheFirstTime)
                    {
                        return RedirectToAction("Edit", "UserTests", new { testId = test.Id });
                    }
                    else
                    {
                        return RedirectToAction("Create", "UserTests", new { testId = test.Id });
                    }
                }
                else // если таск не последний в очереди
                {
                    foreach (var item in test.InputTasks) // ищем следующий по очереди таск
                    {
                        if (item.NumInQueue == (inputTask.NumInQueue + 1))
                        {
                            return RedirectToAction("Display", "InputTasks", new { id = item.Id });
                        }
                    }
                    foreach (var item in test.Tasks) // ищем следующий по очереди таск
                    {
                        if (item.NumInQueue == (inputTask.NumInQueue + 1))
                        {
                            return RedirectToAction("Display", "TrueOutOfFalseTasks", new { id = item.Id });
                        }
                    }
                }
            }
            
            return View(); //??????????????????
        }
    }
}
