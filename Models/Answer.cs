using System.ComponentModel.DataAnnotations;

namespace QuestionsAnswersApp.Models
{
    public class Answer
    {
        [Key]
        public int AnswerId { get; set; }

        public string User { get; set; }
        
        public string AnswerText { get; set; }

        public int UpVotes { get; set; }

        public int DownVotes { get; set; }

        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}