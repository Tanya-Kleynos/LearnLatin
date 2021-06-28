using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class UserTheme
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public ApplicationUser User { get; set; }
        public Theme Theme { get; set; }
        public Int32? Progress { get; set; }
    }
}
