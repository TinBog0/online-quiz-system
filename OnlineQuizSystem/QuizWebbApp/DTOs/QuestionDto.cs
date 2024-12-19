namespace QuizWebbApp.DTOs
{
    public class QuestionDto
    {
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
