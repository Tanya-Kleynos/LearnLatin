using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using LearnLatin.Models;

namespace LearnLatin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<VocabularyUser> VocabularyUsers { get; set; }

        public DbSet<LatinWord> LatinWords { get; set; }

        public DbSet<WordAttachment> WordAttachments { get; set; }
    }
}
