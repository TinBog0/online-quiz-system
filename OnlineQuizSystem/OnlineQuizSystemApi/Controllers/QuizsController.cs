using Microsoft.AspNetCore.Mvc;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Interfaces.Quizes;
using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizsController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuizDto>>> GetAllQuizzes()
        {
            var quizzes = await _quizService.GetAllQuizzesAsync();
            return Ok(quizzes);
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuizDto>> GetQuizById(int id)
        {
            try
            {
                var quiz = await _quizService.GetQuizByIdAsync(id);
                return Ok(quiz);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

       

        // POST: api/Quizs
        [HttpPost]
        public async Task<ActionResult<Quiz>> AddQuiz(QuizDto quizDto)
        {
              await _quizService.AddQuizAsync(quizDto);
              return Ok();            
        }
    }
}
