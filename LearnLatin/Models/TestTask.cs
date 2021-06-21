using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class TestTask
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Editor { get; set; }
    }
}
