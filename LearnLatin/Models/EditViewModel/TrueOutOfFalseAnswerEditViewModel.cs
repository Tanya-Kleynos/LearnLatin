using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.EditViewModels
{
    public class TrueOutOfFalseAnswerEditViewModel
    {
        [Required]
        public String TrueAnswers { get; set; }
        [Required]
        public String FalseAnswers { get; set; }
    }
}
