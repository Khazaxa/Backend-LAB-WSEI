using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class QuizUserServiceEF (
    QuizDbContext _context) : IQuizUserService
{
    public IEnumerable<Quiz> FindAllQuizzes()
    {
        return _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .Select(QuizMapper.FromEntityToQuiz)
            .ToList();
    }

    public Quiz CreateAndGetQuizRandom(int id)
    {
        // Implement this method
        throw new NotImplementedException();
    }

    public Quiz? FindQuizById(int id)
    {
        var entity = _context
            .Quizzes
            .AsNoTracking()
            .Include(q => q.Items)
            .ThenInclude(i => i.IncorrectAnswers)
            .FirstOrDefault(e => e.Id == id);
        return entity is null ? null : QuizMapper.FromEntityToQuiz(entity);
    }

    public List<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId)
    {
        return _context.UserAnswers
            .AsNoTracking()
            .Where(a => a.QuizId == quizId && a.UserId == userId)
            .Select(QuizMapper.FromEntityToQuizItemUserAnswer)
            .ToList();
    }

    public QuizItemUserAnswer SaveUserAnswerForQuiz(int quizId, int quizItemId, int userId, string answer)
    {
        var entity = new QuizItemUserAnswerEntity()
        {
            QuizId = quizId,
            UserAnswer = answer,
            UserId = userId,
            QuizItemId = quizItemId
        };
        var savedEntity = _context.Add(entity).Entity;
        _context.SaveChanges();
        return new QuizItemUserAnswer()
        {
            UserId = savedEntity.UserId,
            QuizId = savedEntity.QuizId,
            QuizItemId = savedEntity.QuizItemId,
            UserAnswer = savedEntity.UserAnswer
        };
    }
}