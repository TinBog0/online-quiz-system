using QuizWebbApp.DTOs;
namespace QuizWebbApp.Interface
{
    public interface IQuizFacade
    {
        QuizDisplayModel GetQuizForDisplay(int quizId);

    }
}
