using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using AutoMapper;
using Infrastructure.Entites;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuizUserServiceEF (
    QuizDbContext _context,
    IMapper _mapper): IQuizUserService
{
    public Quiz CreateAndGetQuizRandom(int count)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Quiz> FindAllQuizzes()
    {
        return _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .Select(entity => _mapper.Map<Quiz>(entity))
            .ToList();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
        return entity is null ? null : _mapper.Map<Quiz>(entity);
    }

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        var quiz = _context.Quizzes.Find(quizId);
        var item = _context.QuizItems.Find(quizItemId);
        if (quiz is null)
        {
            throw new QuizNotFoundException($"Quiz with id = {quizId} not found!");
        }

        if (item is null)
        {
            throw new QuizItemNotFoundException($"Quiz item with id = {quizItemId} not found!");
        }

        var userAnswer = new QuizItemUserAnswerEntity()
        {
            QuizItem = _mapper.Map<QuizItemEntity>(item),
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId
        };
        _context.UserAnswers.Add(userAnswer);
        _context.SaveChanges();
        return _mapper.Map<QuizItemUserAnswer>(userAnswer);
    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        var userAnswersEntities = _context.UserAnswers
            .Where(answer => answer.QuizId == quizId && answer.UserId == userId)
            .ToList();
        
        var userAnswers = userAnswersEntities
            .Select(answerEntity => _mapper.Map<QuizItemUserAnswer>(answerEntity))
            .ToList();

        return userAnswers;
    }
}