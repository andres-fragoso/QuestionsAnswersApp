using Microsoft.EntityFrameworkCore;
using QuestionsAnswersApp.Models;

namespace QuestionsAnswersApp.Data
{
    public class DbInitializer
    {
        private readonly ModelBuilder _builder;

        public DbInitializer(ModelBuilder builder)
        {
            _builder = builder;
        }

        public void Seed()
        {
            _builder.Entity<Question>(a =>
            {
                a.HasData(new Question
                {
                    QuestionId = 1,
                    User = "user1",
                    QuestionText = "Question 1",
                    QuestionTags = "tag1,tag2"
                });
                a.HasData(new Question
                {
                    QuestionId = 2,
                    User = "user1",
                    QuestionText = "Question 2",
                    QuestionTags = "tag2,tag3"
                });
                a.HasData(new Question
                {
                    QuestionId = 3,
                    User = "user2",
                    QuestionText = "Question 3",
                    QuestionTags = "tag3,tag4"
                });
            });

            _builder.Entity<Answer>(b =>
            {
                b.HasData(new Answer
                {
                    AnswerId = 1,
                    AnswerText = "Answer 1 for Question 1",
                    User = "user3",
                    QuestionId = 1
                });
                b.HasData(new Answer
                {
                    AnswerId = 2,
                    AnswerText = "Answer 2 for Question 1",
                    User = "user4",
                    QuestionId = 1
                });
                b.HasData(new Answer
                {
                    AnswerId = 3,
                    AnswerText = "Answer 1 for Question 2",
                    User = "user3",
                    QuestionId = 2
                });
            });

        }
    }
}
