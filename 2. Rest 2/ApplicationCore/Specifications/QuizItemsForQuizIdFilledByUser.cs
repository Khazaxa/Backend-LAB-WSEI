using System.Linq.Expressions;
using ApplicationCore.Commons.Specification;
using ApplicationCore.Interfaces.UserService;
using ApplicationCore.Models;

namespace ApplicationCore.Interfaces.Criteria;

public class QuizItemsForQuizIdFilledByUser: BaseSpecification<QuizItemUserAnswer>
{
    public QuizItemsForQuizIdFilledByUser(int quizId, int userId) : base(answer => answer.QuizId == quizId && answer.UserId == userId)
    {
        AddInclude(answer => answer.QuizItem);
        AddOrderBy(answer => answer.QuizItem.Id);
    }
}