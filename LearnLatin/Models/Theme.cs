using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class Theme
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public String Name { get; set; }
        public String Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public ApplicationUser Creator { get; set; }
        public ApplicationUser Editor { get; set; }
        public ICollection<Test> Tests { get; set; }
        public Int32? NumOfTests { get; set; }
        public Double? PercentageProgress { get; set; }
        public Guid? ParentThemeId { get; set; }
        public Theme ParentTheme { get; set; }
        public ICollection<Theme> Children { get; set; }
        public ICollection<TheoryBlock> TheoryBlocks { get; set; }
    }
}
