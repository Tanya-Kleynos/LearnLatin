using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LearnLatin.Models.EditViewModel
{
    public class ThemeEditViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
    }
}
