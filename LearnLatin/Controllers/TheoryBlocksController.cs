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
    public class TheoryBlocksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TheoryBlocksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: TheoryBlocks
        public async Task<IActionResult> Index()
        {
            return View(await _context.TheoryBlocks.ToListAsync());
        }

        // GET: TheoryBlocks/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theoryBlock = await _context.TheoryBlocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theoryBlock == null)
            {
                return NotFound();
            }

            return View(theoryBlock);
        }

        // GET: TheoryBlocks/Create
        public async Task<IActionResult> Create(Guid? id)
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
            ViewBag.Theme = theme;
            return View(new TheoryBlockCreateViewModel());
        }

        // POST: TheoryBlocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid? id, TheoryBlockCreateViewModel model)
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
            var user = await this._userManager.GetUserAsync(this.HttpContext.User);

            if (this.ModelState.IsValid)
            {
                var theoryBlock = new TheoryBlock
                {
                    Name = model.Name,
                    Text = model.Text,
                    Theme = theme

                };

                this._context.Add(theoryBlock);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction("Details", "Themes", new { id = theme.Id });
            }

            ViewBag.Theme = theme;
            return View(model);
        }

        // GET: TheoryBlocks/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theoryBlock = await _context.TheoryBlocks.FindAsync(id);
            if (theoryBlock == null)
            {
                return NotFound();
            }
            return View(theoryBlock);
        }

        // POST: TheoryBlocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Text")] TheoryBlock theoryBlock)
        {
            if (id != theoryBlock.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theoryBlock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheoryBlockExists(theoryBlock.Id))
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
            return View(theoryBlock);
        }

        // GET: TheoryBlocks/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theoryBlock = await _context.TheoryBlocks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theoryBlock == null)
            {
                return NotFound();
            }

            return View(theoryBlock);
        }

        // POST: TheoryBlocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var theoryBlock = await _context.TheoryBlocks.FindAsync(id);
            _context.TheoryBlocks.Remove(theoryBlock);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheoryBlockExists(Guid id)
        {
            return _context.TheoryBlocks.Any(e => e.Id == id);
        }
    }
}
