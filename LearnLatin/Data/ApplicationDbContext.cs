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
        public DbSet<TrueOutOfFalseTask> TrueOutOfFalseTasks { get; set; }
        public DbSet<InputTask> InputTasks { get; set; }
        public DbSet<TrueOutOfFalseAnswer> TrueOutOfFalseAnswers { get; set; }
        public DbSet<InputAnswer> InputAnswers { get; set; }
        public DbSet<Test> Tests { get; set; } 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
