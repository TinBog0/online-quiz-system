using QuizWebbApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizWebbApp.Services
{
    // OCP -- extending without changing existing code
    // LSP -- if other service is needed we can inject this interface and quizroom woruld work
    // ISP -- small interface, methods related to quiz
    // DIP -- 
    public interface IQuizService
    {
        Task<IEnumerable<QuizDto>> GetAllQuizzesAsync();
        Task<QuizDto> GetQuizByIdAsync(int id);
        Task<bool> SubmitAnswerAsync(int quizId, int questionIndex, string userAnswer); 
    }
}
