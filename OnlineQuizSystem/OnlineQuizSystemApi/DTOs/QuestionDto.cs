using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineQuizSystemApi.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }

        [Required]
        public string QuestionText { get; set; }

        [Required]
        public int Points { get; set; }

        [Required]
        [ExcludeFromCodeCoverage]
        public List<AnswerDto> Answers { get; set; }
    }
}
