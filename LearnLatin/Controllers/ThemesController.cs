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
            return View(await _context.Themes.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

            return View(theme);
        }

        // GET: Themes/Create
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
                    PercentageProgress = 0
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
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Created,Modified,NumOfTests,PercentageProgress")] Theme theme)
        {
            if (id != theme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.Id))
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
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private bool ThemeExists(Guid id)
        {
            return _context.Themes.Any(e => e.Id == id);
        }
    }
}
