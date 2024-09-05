namespace FjsGram.Data;

public class Post
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public List<Image> Images { get; init; } = [];
}
