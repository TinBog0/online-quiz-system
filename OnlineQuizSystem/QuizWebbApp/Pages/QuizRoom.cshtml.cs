using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebbApp.DTOs;
using QuizWebbApp.Interface;
using QuizWebbApp.Services; // For IQuizService
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace QuizWebbApp.Pages
{
    public class QuizRoom : PageModel
    {
        private readonly IQuizFacade _quizFacade;
        private readonly IQuizService _quizService; // We'll need IQuizService for the command

        [BindProperty(SupportsGet = true)]
        public int QuizId { get; set; }

        [BindProperty]
        public string UserAnswer { get; set; }

        public QuizDisplayModel QuizDisplay { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        [BindProperty]
        public int CurrentQuestionIndex { get; set; } = 0;

        public QuizRoom(IQuizFacade quizFacade, IQuizService quizService)
        {
            _quizFacade = quizFacade;
            _quizService = quizService;
        }
        [ExcludeFromCodeCoverage]
        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            if (TempData.ContainsKey("CurrentQuestionIndex"))
            {
                CurrentQuestionIndex = (int)TempData["CurrentQuestionIndex"];
            }
        }

        public IActionResult OnGet()
        {
            if (QuizId <= 0)
            {
                TempData["Error"] = "Invalid Quiz ID.";
                return RedirectToPage("/Index");
            }

            QuizDisplay = _quizFacade.GetQuizForDisplay(QuizId);
            if (QuizDisplay == null)
            {
                TempData["Error"] = $"Quiz with ID {QuizId} does not exist.";
                return RedirectToPage("/Index");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (QuizId <= 0)
            {
                ErrorMessage = "Invalid Quiz ID.";
                return Page();
            }
            if (string.IsNullOrWhiteSpace(UserAnswer))
            {
                ErrorMessage = "Answer cannot be empty.";
                return Page();
            }

            // Re-fetch quiz data for display
            QuizDisplay = _quizFacade.GetQuizForDisplay(QuizId);
            if (QuizDisplay == null)
            {
                ErrorMessage = "Quiz not found.";
                return Page();
            }

            if (CurrentQuestionIndex < 0 || CurrentQuestionIndex >= QuizDisplay.Questions.Count)
            {
                ErrorMessage = "Invalid question index.";
                return Page();
            }

            // 1) Create the command to submit the answer
            var command = new SubmitAnswerCommand(
                quizId: QuizId,
                questionIndex: CurrentQuestionIndex,
                userAnswer: UserAnswer,
                _quizService // pass the actual service reference
            );

            // 2) Execute the command
            command.Execute();

            // 3) For demonstration, move on to the next question
            CurrentQuestionIndex++;
            TempData["CurrentQuestionIndex"] = CurrentQuestionIndex;

            // 4) Provide feedback
            SuccessMessage = $"You answered: {UserAnswer} (Command executed)";

            return Page();
        }
    }
}
