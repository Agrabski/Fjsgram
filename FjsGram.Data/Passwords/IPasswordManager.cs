namespace FjsGram.Data.Passwords;
public interface IPasswordManager
{
    bool VerifyPassword(string hashed, string input);
}
