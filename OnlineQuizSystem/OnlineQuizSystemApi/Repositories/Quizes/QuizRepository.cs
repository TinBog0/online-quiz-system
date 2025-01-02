using Microsoft.EntityFrameworkCore;
using OnlineQuizSystemApi.Interfaces.Books;
using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Repositories.Quizes
{
    public class QuizRepository : IQuizRepository
    {
        private readonly OnlineQuizSystemContext _context;

        public QuizRepository(OnlineQuizSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes
            .Include(q => q.Questions)
            .ThenInclude(q => q.Answers)
            .ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.Id == id);
        }
        public async Task AddQuizAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }
    }
}
