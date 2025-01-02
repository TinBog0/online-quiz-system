using System.ComponentModel.DataAnnotations;

namespace OnlineQuizSystemApi.DTOs
{
    public class QuizDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public int ProfessorId { get; set; }

        [Required]
        public int? TimeLimit { get; set; } 

        [Required]
        public bool IsPublished { get; set; }

        [Required]
        public List<QuestionDto> Questions { get; set; }
    }
}
