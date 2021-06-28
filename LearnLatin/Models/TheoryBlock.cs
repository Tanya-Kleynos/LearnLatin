using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class TheoryBlock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String Text { get; set; }
        public Theme Theme { get; set; }

    }
}
