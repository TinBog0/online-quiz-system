namespace QuizWebbApp.DTOs
{
    public class QuizDisplayModel
    {
        public int QuizId { get; set; }
        public string QuizTitle { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
    public class QuestionDto
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
