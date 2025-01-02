using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Interfaces.Books
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizByIdAsync(int id);
        Task AddQuizAsync(Quiz quiz);
    }
}
