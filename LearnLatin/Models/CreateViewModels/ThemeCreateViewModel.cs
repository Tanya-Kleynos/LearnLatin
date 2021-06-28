using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.CreateViewModels
{
    public class ThemeCreateViewModel
    {
        [Required]
        public String Name { get; set; }
        [Required]
        public String Description { get; set; }
        public Guid ParentThemeId { get; set; }
    }
}
