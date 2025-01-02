using AutoMapper;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Interfaces.Books;
using OnlineQuizSystemApi.Interfaces.Quizes;
using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Repositories.Quizes
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizService(IQuizRepository quizRepository, IMapper mapper)
        {
            _quizRepository = quizRepository;
            _mapper = mapper;
        }

        public async Task AddQuizAsync(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);
            await _quizRepository.AddQuizAsync(quiz);
        }

        public async Task<IEnumerable<QuizDto>> GetAllQuizzesAsync()
        {
            var quizzes = await _quizRepository.GetAllQuizzesAsync();
            return _mapper.Map<IEnumerable<QuizDto>>(quizzes);
        }

        public async Task<QuizDto> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                throw new KeyNotFoundException($"Quiz with ID {id} not found.");
            }
            return _mapper.Map<QuizDto>(quiz);
        }
    }
}
