using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models.ViewModels
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }
        public Guid TestId { get; set; }
        public String Description { get; set; }
        public ICollection<TrueOutOfFalseAnswer> Answers { get; set; }
    }
}
