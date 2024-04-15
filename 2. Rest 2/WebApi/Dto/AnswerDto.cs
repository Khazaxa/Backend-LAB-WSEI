namespace WebApi.Dto;

public class AnswerDto
{
    
    public string Question { get; set; }
    public string Answer { get; set; }
    public bool IsCorrect { get; set; }
}