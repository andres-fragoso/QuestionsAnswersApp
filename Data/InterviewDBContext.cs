using Microsoft.EntityFrameworkCore;
using QuestionsAnswersApp.Models;

namespace QuestionsAnswersApp.Data;

public partial class InterviewDBContext : DbContext
{
    public InterviewDBContext()
    {
    }

    public InterviewDBContext(DbContextOptions<InterviewDBContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Answer>()
            .HasOne(x => x.Question)
            .WithMany(x => x.Answers);

        // Seed database with sample questions and answers
        new DbInitializer(builder).Seed();
    }
}
