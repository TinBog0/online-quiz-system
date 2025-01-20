namespace OnlineQuizSystemApi.Grading
{
    public interface IGradingStrategy
    {
        string CalculateGrade(int correctAnswers, int totalQuestions);
    }
}
