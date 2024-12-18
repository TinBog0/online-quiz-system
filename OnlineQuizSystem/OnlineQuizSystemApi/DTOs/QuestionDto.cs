using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystemApi.DTOs
{
    public class QuestionDto
    {
        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        public List<AnswerDto> Answers { get; set; }
    }
}
