using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace LearnLatin.Models
{
    public class WordAttachment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid LatinWordId { get; set; }

        public LatinWord LatinWord { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public String Path { get; set; }
    }
}
