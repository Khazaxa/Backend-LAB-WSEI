namespace Infrastructure.Entites;

public class QuizEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public ISet<QuizItemEntity> Items { get; set; } = new HashSet<QuizItemEntity>();
}