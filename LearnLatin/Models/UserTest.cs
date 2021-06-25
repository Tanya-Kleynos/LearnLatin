using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class UserTest
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public ApplicationUser User { get; set; }
        public Guid TestId { get; set; } = Guid.NewGuid();
        public Test Test { get; set; }
        public Int32? LastResult { get; set; }
        public Int32? BestResult { get; set; }
    }
}
