using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    
    public class TrueOutOfFalseAnswer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Editor { get; set; }
        public TrueOutOfFalseTask Task { get; set; }
        public String AnsValue { get; set; }
        public Boolean IsTrue { get; set; }
        
    }
}
