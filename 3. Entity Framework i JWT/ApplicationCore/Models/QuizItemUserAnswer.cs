using ApplicationCore.Interfaces.Repository;

namespace ApplicationCore.Models;

public class QuizItemUserAnswer: IIdentity<string>
{
    public int QuizId { get; init; }
    public QuizItem  QuizItem{ get; init; }
    public int UserId { get; init; }
    public string Answer { get; init; }
    
    public QuizItemUserAnswer()
    {
    }
    
    public QuizItemUserAnswer(QuizItem quizItem, int userId, int quizId,string answer)
    {
        QuizItem = quizItem;
        Answer = answer;
        UserId = userId;
        QuizId = quizId;
    }
    
    public bool IsCorrect()
    {
        return QuizItem.CorrectAnswer == Answer;
    }
    public string Id
    {
        get => QuizItem != null ? $"{QuizId}{UserId}{QuizItem.Id}" : $"{QuizId}{UserId}";
        set
        {
        
        }
    }
}