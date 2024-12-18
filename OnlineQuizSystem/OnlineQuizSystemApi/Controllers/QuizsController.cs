using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizsController : ControllerBase
    {
        private readonly OnlineQuizSystemContext _context;

        public QuizsController(OnlineQuizSystemContext context)
        {
            _context = context;
        }

        // GET: api/Quizs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quiz>>> GetQuizzes()
        {
            return await _context.Quizzes.ToListAsync();
        }

        // GET: api/Quizs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Quiz>> GetQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);

            if (quiz == null)
            {
                return NotFound();
            }

            return quiz;
        }

        // PUT: api/Quizs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuiz(int id, Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return BadRequest();
            }

            _context.Entry(quiz).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Quizs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Quiz>> PostQuiz(QuizDto quizDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quiz = new Quiz
            {
                Title = quizDto.Title,
                Description = quizDto.Description,
                CourseId = quizDto.CourseId,
                ProfessorId = quizDto.ProfessorId,
                DateCreated = DateTime.Now,
                Questions = quizDto.Questions.Select(q => new Question
                {
                    QuestionText = q.QuestionText,
                    Points = q.Points,
                    Answers = q.Answers.Select(a => new Answer
                    {
                        AnswerText = a.AnswerText,
                        IsCorrect = a.IsCorrect
                    }).ToList()
                }).ToList()
            };

            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        // DELETE: api/Quizs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuiz(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }

            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.Id == id);
        }
    }
}
