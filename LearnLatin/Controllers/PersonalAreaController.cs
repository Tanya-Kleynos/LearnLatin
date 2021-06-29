using LearnLatin.Data;
using LearnLatin.Models;
using LearnLatin.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Controllers
{
    [Authorize]
    public class PersonalAreaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PersonalAreaController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET: PersonalAreaController
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(this.HttpContext.User);
            var themes = await _context.Themes.ToListAsync();
            var personalAreaModel = new PersonalAreaViewModel
            {
                Themes = themes
            };

            return View(personalAreaModel);
        }
        public async Task<IActionResult> TrainingResults(Guid trainingId)
        {
            /*if (trainingId == null)
            {
                return NotFound();
            }

            var training = await _context.Trainings
                .Include(g => g.Games)
                .ThenInclude(g => g.Level)
                .Include(g => g.Player)
                .Where(g => g.Id == trainingId)
                .FirstOrDefaultAsync(m => m.Id == trainingId);

            if (training == null)
            {
                return NotFound();
            }*/

            return View();
        }
    }
}
