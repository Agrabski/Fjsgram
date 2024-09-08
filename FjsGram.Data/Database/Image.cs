namespace FjsGram.Data.Database;

public class Image
{
    public required Guid Id { get; set; }
    public required byte[] Data { get; set; }
}
