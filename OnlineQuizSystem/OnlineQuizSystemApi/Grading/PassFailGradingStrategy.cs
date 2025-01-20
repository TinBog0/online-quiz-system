namespace OnlineQuizSystemApi.Grading
{
    public class PassFailGradingStrategy : IGradingStrategy
    {
        public string CalculateGrade(int correctAnswers, int totalQuestions)
        {
            var passThreshold = totalQuestions * 0.5;
            return correctAnswers >= passThreshold ? "Grade: Pass" : "Grade: Fail";
        }
    }

}
