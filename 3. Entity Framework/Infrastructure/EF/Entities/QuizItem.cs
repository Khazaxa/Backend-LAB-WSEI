using ApplicationCore.Interfaces.Repository;

namespace Infrastructure.EF.Entities;

public class QuizItem : IIdentity<int>
{
    public int Id { get; set; }
    public string Question { get; set; }
    public ICollection<IncorrectAnswers> IncorrectAnswers { get; set; }
    public string CorrectAnswer { get; set; }

    public QuizItem()
    {
        IncorrectAnswers = new List<IncorrectAnswers>();
    }
}