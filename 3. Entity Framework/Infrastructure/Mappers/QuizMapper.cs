using ApplicationCore.Models;
using Infrastructure.EF.Entities;
using QuizItem = ApplicationCore.Models.QuizItem;

namespace Infrastructure.Mappers;

public static class QuizMapper
{
    public static QuizItem FromEntityToQuizItem(QuizItemEntity entity)
    {
        return new QuizItem(
            entity.Id,
            entity.Question,
            entity.IncorrectAnswers.Select(e => e.Answer).ToList(),
            entity.CorrectAnswer);
    }

    public static Quiz FromEntityToQuiz(QuizEntity entity)
    {
        return new Quiz(
            entity.Id,
            entity.Title,
            entity.Items.Select(FromEntityToQuizItem).ToList());
    }
    
    public static QuizItemUserAnswer FromEntityToQuizItemUserAnswer(QuizItemUserAnswerEntity entity)
    {
        return new QuizItemUserAnswer()
        {
            UserId = entity.UserId,
            QuizId = entity.QuizId,
            QuizItemId = entity.QuizItemId,
            UserAnswer = entity.UserAnswer
        };
    }
}