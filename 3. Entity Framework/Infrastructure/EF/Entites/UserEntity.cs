using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Entites;

public class UserEntity : IdentityUser<int>
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}