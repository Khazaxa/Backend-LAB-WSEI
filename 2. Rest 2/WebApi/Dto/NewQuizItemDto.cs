namespace WebApi.Dto;

public class NewQuizItemDto
{
    public string Question { get; set; }
    public List<string> Options { get; set; }
    public int CorrectOptionIndex { get; set; }
}