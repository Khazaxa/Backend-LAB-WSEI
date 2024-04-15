namespace Infrastructure.EF.Entities;

public class IncorrectAnswers
{
    public int Id { get; set; }
    public string Answer { get; set; }
    public int QuizItemId { get; set; }
    public QuizItem QuizItem { get; set; }
}