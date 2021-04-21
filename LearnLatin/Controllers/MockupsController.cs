using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Controllers
{
    public class MockupsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult TrainingsAll()
        {
            return this.View();
        }

        public IActionResult VocabularyUser()
        {
            return this.View();
        }

        /*public IActionResult ***()
        {
            return this.View();
        }*/
    }
}
