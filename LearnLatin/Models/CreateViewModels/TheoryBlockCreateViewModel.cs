using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.CreateViewModels
{
    public class TheoryBlockCreateViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Text { get; set; }
    }
}
