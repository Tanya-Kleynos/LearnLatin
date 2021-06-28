using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnLatin.Data;
using LearnLatin.Models;

namespace LearnLatin.Controllers
{
    public class TheoryBlocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TheoryBlocksController(ApplicationDbContext context)
        {
            _context = context;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: TheoryBlocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Text")] TheoryBlock theoryBlock)
        {
            if (ModelState.IsValid)
            {
                theoryBlock.Id = Guid.NewGuid();
                _context.Add(theoryBlock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(theoryBlock);
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
