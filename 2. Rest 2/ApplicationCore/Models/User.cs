using ApplicationCore.Commons.Repository;

namespace  ApplicationCore.Models.QuizAggregate;

public class User: IIdentity<int>
{
    public int Id { get; set; }
    
    public string Username { get; init; }
}