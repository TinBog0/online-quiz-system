using AutoMapper;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Mapping;
using OnlineQuizSystemApi.Models;

namespace Tests
{
    public class MappingProfileTests
    {
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void MappingProfile_ShouldMapQuizDtoToQuiz()
        {
            // Arrange
            var quizDto = new QuizDto
            {
                Id = 1,
                Title = "Sample Quiz",
                Description = "A sample quiz for testing",
                CourseId = 2,
                ProfessorId = 3,
                TimeLimit = 60,
                IsPublished = true,
                Questions = new List<QuestionDto>
            {
                new QuestionDto
                {
                    Id = 1,
                    QuestionText = "What is 2 + 2?",
                    Points = 5,
                    Answers = new List<AnswerDto>
                    {
                        new AnswerDto { Id = 1, AnswerText = "4", IsCorrect = true },
                        new AnswerDto { Id = 2, AnswerText = "5", IsCorrect = false }
                    }
                }
            }
            };

            // Act
            var quiz = _mapper.Map<Quiz>(quizDto);

            // Assert
            Assert.Equal(quizDto.Id, quiz.Id);
            Assert.Equal(quizDto.Title, quiz.Title);
            Assert.Equal(quizDto.Description, quiz.Description);
            Assert.Equal(quizDto.CourseId, quiz.CourseId);
            Assert.Equal(quizDto.ProfessorId, quiz.ProfessorId);
            Assert.Equal(quizDto.TimeLimit, quiz.TimeLimit);
            Assert.Equal(quizDto.IsPublished, quiz.IsPublished);
            Assert.Equal(quizDto.Questions.Count, quiz.Questions.Count);
            Assert.Equal(quizDto.Questions[0].Answers.Count, quiz.Questions.First().Answers.Count);
        }

        [Fact]
        public void MappingProfile_ShouldMapQuestionDtoToQuestion()
        {
            // Arrange
            var questionDto = new QuestionDto
            {
                Id = 1,
                QuestionText = "What is the capital of France?",
                Points = 10,
                Answers = new List<AnswerDto>
            {
                new AnswerDto { Id = 1, AnswerText = "Paris", IsCorrect = true },
                new AnswerDto { Id = 2, AnswerText = "Berlin", IsCorrect = false }
            }
            };

            // Act
            var question = _mapper.Map<Question>(questionDto);

            // Assert
            Assert.Equal(questionDto.Id, question.Id);
            Assert.Equal(questionDto.QuestionText, question.QuestionText);
            Assert.Equal(questionDto.Points, question.Points);
            Assert.Equal(questionDto.Answers.Count, question.Answers.Count);
        }

        [Fact]
        public void MappingProfile_ShouldMapAnswerDtoToAnswer()
        {
            // Arrange
            var answerDto = new AnswerDto
            {
                Id = 1,
                AnswerText = "42",
                IsCorrect = true
            };

            // Act
            var answer = _mapper.Map<Answer>(answerDto);

            // Assert
            Assert.Equal(answerDto.Id, answer.Id);
            Assert.Equal(answerDto.AnswerText, answer.AnswerText);
            Assert.Equal(answerDto.IsCorrect, answer.IsCorrect);
        }

        [Fact]
        public void MappingProfile_ShouldMapQuizToQuizDto()
        {
            // Arrange
            var quiz = new Quiz
            {
                Id = 1,
                Title = "Sample Quiz",
                Description = "A sample quiz for testing",
                CourseId = 2,
                ProfessorId = 3,
                TimeLimit = 60,
                IsPublished = true,
                Questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    QuestionText = "What is 2 + 2?",
                    Points = 5,
                    Answers = new List<Answer>
                    {
                        new Answer { Id = 1, AnswerText = "4", IsCorrect = true },
                        new Answer { Id = 2, AnswerText = "5", IsCorrect = false }
                    }
                }
            }
            };

            // Act
            var quizDto = _mapper.Map<QuizDto>(quiz);

            // Assert
            Assert.Equal(quiz.Id, quizDto.Id);
            Assert.Equal(quiz.Title, quizDto.Title);
            Assert.Equal(quiz.Description, quizDto.Description);
            Assert.Equal(quiz.CourseId, quizDto.CourseId);
            Assert.Equal(quiz.ProfessorId, quizDto.ProfessorId);
            Assert.Equal(quiz.TimeLimit, quizDto.TimeLimit);
            Assert.Equal(quiz.IsPublished, quizDto.IsPublished);
            Assert.Equal(quiz.Questions.Count, quizDto.Questions.Count);
        }
    }
}
