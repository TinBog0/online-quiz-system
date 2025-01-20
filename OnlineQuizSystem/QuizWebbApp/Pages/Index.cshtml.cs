using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebbApp.Services;
using QuizWebbApp.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

//SRP -- fetching and displaying quizs

namespace QuizWebbApp.Pages
{
    public class IndexPage : PageModel
    {
        private readonly IQuizService _quizService;

        public IEnumerable<QuizDto> Quizzes { get; set; }

        public IndexPage(IQuizService quizService)
        {
            _quizService = quizService;
        }

        public async Task OnGetAsync()
        {
            Quizzes = await _quizService.GetAllQuizzesAsync();
        }
    }
}
