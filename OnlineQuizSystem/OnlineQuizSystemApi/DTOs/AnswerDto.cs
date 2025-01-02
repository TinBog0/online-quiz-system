using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystemApi.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }

        [Required]
        public string AnswerText { get; set; }

        [Required]
        public bool IsCorrect { get; set; }
    }
}
