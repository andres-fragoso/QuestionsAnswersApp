using System.ComponentModel.DataAnnotations;

namespace QuestionsAnswersApp.Models
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        public string User { get; set; }

        public string QuestionText { get; set; }
        
        public string? QuestionTags { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

    }
}