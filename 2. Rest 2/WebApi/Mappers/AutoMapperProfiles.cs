using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using AutoMapper;
using WebApi.Dto;

namespace WebApi.Mappers;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Quiz, QuizDto>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items)); // Dodajemy to mapowanie

        CreateMap<QuizItem, QuizItemDto>();

        CreateMap<QuizItemUserAnswer, AnswerDto>()
            .ForMember(dest 
                => dest.Question, opt 
                => opt.MapFrom(src => src.QuizItem.Question))
            .ForMember(dest 
                => dest.Answer, opt 
                => opt.MapFrom(src => src.Answer))
            .ForMember(dest 
                => dest.IsCorrect, opt 
                => opt.MapFrom(src => src.IsCorrect()));

        CreateMap<(int QuizId, int UserId, int TotalQuestions, IEnumerable<QuizItemUserAnswer> Feedback), FeedbackDto>()
            .ForMember(dest 
                => dest.QuizId, opt 
                => opt.MapFrom(src => src.QuizId))
            .ForMember(dest 
                => dest.UserId, opt 
                => opt.MapFrom(src => src.UserId))
            .ForMember(dest 
                => dest.TotalQuestions, opt 
                => opt.MapFrom(src => src.TotalQuestions))
            .ForMember(dest 
                => dest.Answers, opt 
                => opt.MapFrom(src => src.Feedback));
    }
}