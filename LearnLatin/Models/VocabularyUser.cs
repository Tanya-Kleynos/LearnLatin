using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LearnLatin.Models
{
    public class VocabularyUser
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public String OwnerId { get; set; }

        public ApplicationUser Owner { get; set; }

        public ICollection<LatinWord> LatinWords { get; set; }
    }
}
