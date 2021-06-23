using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.EditViewModels
{
    public class TaskEditViewModel
    {
        [Required]
        public String Description { get; set; }
    }
}
