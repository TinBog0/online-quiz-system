using QuizWebbApp.Interface;

namespace QuizWebbApp.Services
{
    public class SubmitAnswerCommand : ICommand
    {
        private readonly int _quizId;
        private readonly int _questionIndex;
        private readonly string _userAnswer;

        private readonly IQuizService _quizService; // or IQuizFacade if you prefer

        public SubmitAnswerCommand(int quizId, int questionIndex, string userAnswer, IQuizService quizService)
        {
            _quizId = quizId;
            _questionIndex = questionIndex;
            _userAnswer = userAnswer;
            _quizService = quizService;
        }

        public void Execute()
        {
            // Actually submit or store the answer
            // For demo, just calling an async method via .Result. 
            // In production, consider making this command async or handle properly.

            bool success = _quizService.SubmitAnswerAsync(_quizId, _questionIndex, _userAnswer)
                .GetAwaiter().GetResult();

            // Optionally do logging or other post-submit steps
            Console.WriteLine($"SubmitAnswerCommand executed. Success={success}");
        }
    }

}
