﻿using Microsoft.EntityFrameworkCore;
using MultiLanguageExamManagementSystem.Models.Entities;

namespace MultiLanguageExamManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<LocalizationResource> LocalizationResources { get; set; }
		public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamQuestion> ExamQuestions { get; set; }
        public DbSet<TakenExam> TakenExams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Your code here
            modelBuilder.Entity<Country>()
            	.HasMany(c => c.Languages)
            	.WithOne(l => l.Country)
            	.HasForeignKey(l => l.CountryId)
            	.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Language>()
            	.HasMany(l => l.LocalizationResources)
            	.WithOne(r => r.Language)
            	.HasForeignKey(r => r.LanguageId)
            	.OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LocalizationResource>()
            	.HasIndex(lr => new { lr.Namespace, lr.Key })
            	.IsUnique();

			modelBuilder.Entity<ExamQuestion>()
                .HasKey(eq => new { eq.ExamId, eq.QuestionId });

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Exam)
                .WithMany(e => e.ExamQuestions)
                .HasForeignKey(eq => eq.ExamId);

            modelBuilder.Entity<ExamQuestion>()
                .HasOne(eq => eq.Question)
                .WithMany(q => q.ExamQuestions)
                .HasForeignKey(eq => eq.QuestionId);
        }

    }
}
