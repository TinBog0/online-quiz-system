using QuizWebbApp.DTOs;

namespace QuizWebbApp.Services {
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(int id);
    }
}
