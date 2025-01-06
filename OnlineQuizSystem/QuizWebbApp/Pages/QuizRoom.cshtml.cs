using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebbApp.DTOs;
using QuizWebbApp.Services;
using System.Threading.Tasks;

//SRP -- only resp for handling quiz logic -- IquizS, quizS respo for fetching


namespace QuizWebbApp.Pages
{
    public class QuizRoom : PageModel
    {
        private readonly IQuizService _quizService;

        [BindProperty(SupportsGet = true)]
        public int QuizId { get; set; }

        [BindProperty]
        public string UserAnswer { get; set; }

        public QuizDto Quiz { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        // Track current question index
        [BindProperty]
        public int CurrentQuestionIndex { get; set; } = 0;

        public QuizRoom(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (QuizId <= 0)
            {
                TempData["Error"] = "Invalid Quiz ID.";
                return RedirectToPage("/Index");
            }

            Quiz = await _quizService.GetQuizByIdAsync(QuizId);
            if (Quiz == null)
            {
                TempData["Error"] = $"Quiz with ID {QuizId} does not exist.";
                return RedirectToPage("/Index");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
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

            Quiz = await _quizService.GetQuizByIdAsync(QuizId);
            if (Quiz == null)
            {
                ErrorMessage = "Quiz not found.";
                return Page();
            }

            if (CurrentQuestionIndex < 0 || CurrentQuestionIndex >= Quiz.Questions.Count)
            {
                ErrorMessage = "Invalid question index.";
                return Page();
            }

            var currentQuestion = Quiz.Questions[CurrentQuestionIndex];

            CurrentQuestionIndex++;

            TempData["CurrentQuestionIndex"] = CurrentQuestionIndex;


            return Page();
        }

        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            if (TempData.ContainsKey("CurrentQuestionIndex"))
            {
                CurrentQuestionIndex = (int)TempData["CurrentQuestionIndex"];
            }
        }
    }
}
