using System.ComponentModel.DataAnnotations;

namespace WebApi.Dto;

public class QuizItemDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; } 
}