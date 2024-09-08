namespace FjsGram.Data.Database;

public class User
{
    public required string Login { get; init; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public List<Post> Posts { get; init; } = [];
}
