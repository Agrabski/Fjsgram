using FjsGram.Data.Database;
using FjsGram.Data.Passwords;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FjsGram.Server.Areas;

public static class Accounts
{
    public static WebApplication AddAccountArea(this WebApplication application)
    {
        var area = application.MapGroup("account");
        area
            .MapPost("login", LoginAsync)
            .AllowAnonymous()
        ;
        area
            .MapPost("register", RegisterAsync)
            .AllowAnonymous()
        ;
        return application;
    }

    private static async Task<IResult> LoginAsync(
        [FromForm] string login,
        [FromForm] string password,
        [FromServices] FjsGramContext context,
        [FromServices] IPasswordManager passwordManager,
        CancellationToken token
        )
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login, token);
        if (user is null)
            return Results.Unauthorized();
        if (!passwordManager.VerifyPassword(user.PasswordHash, password))
            return Results.Unauthorized();
        //todo: cookie
        return Results.Redirect("/");
    }
    private static async Task<IResult> RegisterAsync(
        [FromForm] string login,
        [FromForm] string email,
        [FromForm] string password,
        [FromForm(Name = "confirm-password")] string confirmPassword,
        [FromServices] FjsGramContext context,
        CancellationToken token
        )
    {
        return Results.Redirect("/");
    }
}
