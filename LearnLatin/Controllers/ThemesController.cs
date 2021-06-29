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
using Microsoft.AspNetCore.Authorization;
using LearnLatin.Models.ViewModels;

namespace LearnLatin.Controllers
{
    public class ThemesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public ThemesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Themes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Themes
                .Include(t => t.ParentTheme)
                .Include(t => t.Creator)
                .Include(t => t.Editor)
                .ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .Include(t => t.Tests)
                .Include(t => t.TheoryBlocks)
                .Include(x => x.Creator)
                .Include(x => x.Editor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }
            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            var userTheme = await _context.UserThemes
                .Where(u => u.User.Id == user.Id)
                .Where(t => t.Theme.Id == theme.Id)
                .SingleOrDefaultAsync();

            /*if (userTheme == null)
            {
                return NotFound();
            }*/
            var viewModel = new ThemeViewModel
            {
                Theme = theme,
                UserTheme = userTheme,
                TheoryBlocks = theme.TheoryBlocks
            };
            return View(viewModel);
        }
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> AddTest(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Name");
            ViewBag.Theme = theme;
            return View(new TestAddViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> AddTest(Guid? id, TestAddViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (theme == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var test = await _context.Tests
                    .Include(t => t.Theme)
                .FirstOrDefaultAsync(m => m.Id == model.TestId);
                test.Theme = theme;
                if (test.Theme.NumOfTests == null)
                {
                    test.Theme.NumOfTests = 1;
                }
                else
                {
                    test.Theme.NumOfTests++;
                }
                await this._context.SaveChangesAsync();

                return this.RedirectToAction("Details", "Themes", new { id = id });
            }


            ViewData["TestId"] = new SelectList(_context.Tests, "Id", "Name");
            ViewBag.Theme = theme;
            return View(new TestAddViewModel());
        }

        // GET: Themes/Create
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public IActionResult Create()
        {
            //this.ViewBag.Theme = theme;

            //.Where(x => !x.ParentTheme.Any(z => z.ParentTheme.Id == hospitalId));

            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");

            return View(new ThemeCreateViewModel());
        }

        // POST: Themes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Create(ThemeCreateViewModel model)
        {
            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var theme = new Theme
                {
                    Name = model.Name,
                    Description = model.Description,
                    Created = DateTime.Now,
                    Creator = user,
                    Editor = user,
                    Modified = DateTime.Now,
                    NumOfTests = 0,
                };

                if (model.ParentThemeId != null)
                {
                    theme.ParentThemeId = model.ParentThemeId;
                }

                this._context.Add(theme);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Index", "PersonalArea");
            }
            var ListOfThemes = _context.Themes.ToListAsync();
            //.Where(x => !x.ParentTheme.Any(z => z.ParentTheme.Id == hospitalId));
            this.ViewData["ThemeId"] = new SelectList((System.Collections.IEnumerable)ListOfThemes, "Id", "Name");
            return View(model);
        }

        // GET: Themes/Edit/5
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            var model = new ThemeCreateViewModel
            {
                Name = theme.Name,
                Description = theme.Description
            };
            ViewData["ThemeId"] = new SelectList(_context.Themes, "Id", "Name");
            return View(model);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Edit(Guid id, ThemeCreateViewModel model)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                /*.Include(a => a.Task)*/
                .FirstOrDefaultAsync(m => m.Id == id);

            if (theme == null)
            {
                return NotFound();
            }

            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (ModelState.IsValid)
            {
                theme.Name = model.Name;
                theme.Description = model.Description;
                theme.ParentThemeId = model.ParentThemeId;
                theme.Modified = DateTime.Now;
                theme.Editor = user;

                await this._context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Themes/Delete/5
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .Include(t => t.Creator)
                .Include(t => t.Editor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = ApplicationRoles.Administrators)]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var theme = await _context.Themes.FindAsync(id);
            var children = await _context.Themes
                .Where(t => t.ParentThemeId == theme.Id)
                .ToListAsync();
            foreach (var item in children)
            {
                item.ParentTheme = null;
                item.ParentThemeId = null;
            }
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}