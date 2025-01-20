using System.Diagnostics.CodeAnalysis;

namespace QuizWebbApp.DTOs
{
    [ExcludeFromCodeCoverage]
    public class AnswerDto
    {
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
    }

}
