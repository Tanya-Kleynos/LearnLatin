using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class Test
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Editor { get; set; }
        public ICollection<TestTask> Tasks { get; set; }
        public Int32? NumOfTasks { get; set; }
    }
}
