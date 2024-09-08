namespace FjsGram.Data.Passwords;
public interface IPasswordManager
{
    string HashPassword(string password);
    bool VerifyPassword(string hashed, string input);
}
