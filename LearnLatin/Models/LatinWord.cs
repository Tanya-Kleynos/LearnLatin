using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnLatin.Models
{
    public class LatinWord
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid VocabularyUserId { get; set; }

        public VocabularyUser VocabularyUser { get; set; }

       
        public String Name { get; set; }

        public String Translation { get; set; }

        public Int32 Percent { get; set; } = 0;

        public Boolean Training { get; set; } = false;


        public WordAttachment WordAttachment { get; set; }

    }
}
