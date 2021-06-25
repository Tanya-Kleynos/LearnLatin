using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.ViewModels
{
    public class InputTaskViewModel
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public String Description { get; set; }
        [Required]
        public String Answer { get; set; }
    }
}
