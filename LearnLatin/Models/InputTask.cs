using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class InputTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Editor { get; set; }
        public Test Test { get; set; }
        public Int32 NumInQueue { get; set; }
        public ICollection<InputAnswer> Answers { get; set; }
       
    }
}
