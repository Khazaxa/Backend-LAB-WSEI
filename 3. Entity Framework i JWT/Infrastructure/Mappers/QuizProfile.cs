using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Entites;

namespace Infrastructure.Mappers;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<QuizEntity, Quiz>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

        CreateMap<QuizItemEntity, QuizItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Question, opt => opt.MapFrom(src => src.Question))
            .ForMember(dest => dest.CorrectAnswer, opt => opt.MapFrom(src => src.CorrectAnswer))
            .ForMember(dest => dest.IncorrectAnswers,
                opt => opt.MapFrom(src => src.IncorrectAnswers.Select(e => e.Answer).ToList()));
        
        CreateMap<QuizItemUserAnswerEntity, QuizItemUserAnswer>()
            .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.UserAnswer))
            .ForMember(dest => dest.QuizItem, opt => opt.MapFrom(src => src.QuizItem));
    }
}
