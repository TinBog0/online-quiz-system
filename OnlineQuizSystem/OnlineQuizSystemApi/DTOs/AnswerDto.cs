using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystemApi.DTOs
{
    public class AnswerDto
    {
        [Required]
        public string AnswerText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
