using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;

namespace ApplicationCore.Interfaces.UserService;

public interface IQuizUserService
{
    Quiz CreateAndGetQuizRandom(int count);

    Quiz? FindQuizById(int id);

    IQueryable<Quiz> FindAll();

    void SaveUserAnswerForQuiz(int quizId, int userId, int quizItemId, string answer);

    IQueryable<QuizItemUserAnswer> GetUserAnswersForQuiz(int quizId, int userId);

    int CountCorrectAnswersForQuizFilledByUser(int quizId, int userId)
    {
        return GetUserAnswersForQuiz(quizId, userId)
            .Count(e => e.IsCorrect());
    }
}