using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizWebbApp.Services;
using QuizWebbApp.DTOs; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizWebbApp.Pages;
public class IndexModel : PageModel
{
    private readonly QuizService _quizService;

    public IEnumerable<QuizDto> Quizzes { get; set; }

    public IndexModel(QuizService quizService)
    {
        _quizService = quizService;
    }

    public async Task OnGetAsync()
    {
        Quizzes = await _quizService.GetAllQuizzesAsync();
    }
}
