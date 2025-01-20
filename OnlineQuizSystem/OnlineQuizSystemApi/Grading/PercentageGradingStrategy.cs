namespace OnlineQuizSystemApi.Grading
{
    public class PercentageGradingStrategy : IGradingStrategy
    {
        public string CalculateGrade(int correctAnswers, int totalQuestions)
        {
            var percentage = (double)correctAnswers / totalQuestions * 100;
            return $"Grade: {percentage:F2}%";
        }
    }

}
