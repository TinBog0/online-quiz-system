using QuizWebbApp.DTOs;


namespace QuizWebbApp.Services
{
    public class ProxyQuizService : IQuizService
    {
        private readonly IQuizService _realService;

        public ProxyQuizService(IQuizService realService)
        {
            _realService = realService;
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            // Example: logging or permission check
            Console.WriteLine("Proxy: Checking permissions for GetAllQuizzesAsync...");
            // Then forward to real service
            var result = await _realService.GetAllQuizzesAsync();
            return result;
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            // Example: caching
            Console.WriteLine($"Proxy: Checking cache for quizId={id}");
            // (Pseudo) if cached, return from cache
            // else forward call
            var quiz = await _realService.GetQuizByIdAsync(id);
            return quiz;
        }

        public async Task<bool> SubmitAnswerAsync(int quizId, int questionIndex, string userAnswer)
        {
            // Example: logging or validation
            Console.WriteLine($"Proxy: Submitting answer for quizId={quizId} questionIndex={questionIndex}");

            // Forward to real service
            bool success = await _realService.SubmitAnswerAsync(quizId, questionIndex, userAnswer);
            Console.WriteLine($"Proxy: SubmitAnswerAsync result={success}");
            return success;
        }
    }
}
