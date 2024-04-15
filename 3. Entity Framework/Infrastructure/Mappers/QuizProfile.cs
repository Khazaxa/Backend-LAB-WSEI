using AutoMapper;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using QuizItem = Infrastructure.EF.Entities.QuizItem;

namespace Infrastructure.Mappers;

public class QuizProfile : Profile
{
    public QuizProfile()
    {
        CreateMap<QuizEntity, Quiz>();
        CreateMap<QuizItemEntity, QuizItem>();
        CreateMap<QuizItemUserAnswerEntity, QuizItemUserAnswer>();
        CreateMap<UserEntity, User>();
    }
}