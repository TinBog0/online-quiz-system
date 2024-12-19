using AutoMapper;
using OnlineQuizSystemApi.DTOs;
using OnlineQuizSystemApi.Models;

namespace OnlineQuizSystemApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Answers
            CreateMap<AnswerDto, Answer>().ReverseMap();

            //Questions
            CreateMap<QuestionDto, Question>().ReverseMap();

            //Quizes
            CreateMap<QuizDto, Quiz>().ReverseMap();
        }
    }
}
