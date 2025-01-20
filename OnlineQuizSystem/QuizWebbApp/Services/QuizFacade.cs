using QuizWebbApp.DTOs;
using QuizWebbApp.Interface;

namespace QuizWebbApp.Services
{
    public class QuizFacade : IQuizFacade
    {
        private readonly IQuizService _quizService;

        public QuizFacade(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public QuizDisplayModel GetQuizForDisplay(int quizId)
        {
            // 1) Retrieve the quiz DTO from the service
            var quizDto = _quizService.GetQuizByIdAsync(quizId).Result;
            if (quizDto == null)
                return null;

            // 2) Convert QuizDto to QuizDisplayModel
            var displayModel = new QuizDisplayModel
            {
                QuizId = quizId,
                QuizTitle = quizDto.Title,
                Questions = quizDto.Questions.Select(q => new QuestionDto
                {
                    QuestionId = q.QuestionId,
                    QuestionText = q.QuestionText,
                }).ToList()
            };

            // 3) Return a view-ready model
            return displayModel;
        }

        
    }

}
