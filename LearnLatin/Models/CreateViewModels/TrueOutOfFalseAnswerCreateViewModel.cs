using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.CreateViewModels
{
    public class TrueOutOfFalseAnswerCreateViewModel
    {
        [Required]
        public String AnsValue { get; set; }
        [Required]
        public Boolean IsTrue { get; set; }
    }
}
