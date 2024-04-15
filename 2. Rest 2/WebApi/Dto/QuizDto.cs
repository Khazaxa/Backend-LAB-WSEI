namespace WebApi.Dto;

public class QuizDto
{
    public int  Id { get; set; }
    public string Title { get; set; }
    public List<QuizItemDto> Items { get; set; }
}