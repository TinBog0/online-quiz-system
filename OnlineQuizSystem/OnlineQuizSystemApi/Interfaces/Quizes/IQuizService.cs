using OnlineQuizSystemApi.DTOs;

namespace OnlineQuizSystemApi.Interfaces.Quizes
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(int id);
        Task AddQuizAsync(QuizDto quizDto);
    }
}
