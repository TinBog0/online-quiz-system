namespace QuizWebbApp.DTOs

{

    public class QuizDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        public int ProfessorId { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
