using ApplicationCore.Interfaces.Repository;

namespace ApplicationCore.Models;

public class Quiz: IIdentity<int>
{
    public int Id { get; set; }
    
    public string Title { get; }
    
    public List<QuizItem> Items { get; }

    public Quiz(int id, string title, List<QuizItem> items)
    {
        Id = id;
        Items = items;
        Title = title;
    }
    
}