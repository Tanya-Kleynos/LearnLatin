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
        public DbSet<UserTest> UserTests { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<TheoryBlock> TheoryBlocks { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<UserTest>()
                .HasKey(x => new { x.UserId, x.TestId });
            builder.Entity<TrueOutOfFalseTask>().HasMany(x => x.Answers).WithOne(t => t.Task).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<InputTask>().HasMany(x => x.Answers).WithOne(t => t.Task).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Test>().HasMany(x => x.Tasks).WithOne(t => t.Test).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Test>().HasMany(x => x.InputTasks).WithOne(t => t.Test).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Theme>().HasMany(x => x.Tests).WithOne(t => t.Theme).OnDelete(DeleteBehavior.Cascade);
           
        }
    }
}
