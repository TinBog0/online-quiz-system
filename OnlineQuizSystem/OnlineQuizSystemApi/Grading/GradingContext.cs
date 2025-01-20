namespace OnlineQuizSystemApi.Grading
{
    public class GradingContext
    {
        private IGradingStrategy _strategy;

        public void SetStrategy(IGradingStrategy strategy)
        {
            _strategy = strategy;
        }

        public string ExecuteGrading(int correctAnswers, int totalQuestions)
        {
            return _strategy.CalculateGrade(correctAnswers, totalQuestions);
        }
    }
}
