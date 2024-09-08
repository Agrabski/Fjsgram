using FjsGram.Data;
using FjsGram.Data.Database;
using FjsGram.Data.Passwords;
using FjsGram.Server.Areas;
using FjsGram.Server.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

builder.Services
    .AddAuthorization()
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        o.SlidingExpiration = true;
        o.AccessDeniedPath = "/Forbidden/";
    })
    .Services
    .AddFjsGramData()
    .AddHostedService<DatabaseMigrationService>()
    .Configure<ArgonOptions>(builder.Configuration.GetSection("argon"))
;
builder.AddSqlServerDbContext<FjsGramContext>("primary", o => o.DisableTracing = true);
var app = builder.Build();

app.MapDefaultEndpoints();
app.UseStaticFiles();
app.UseCookiePolicy(new CookiePolicyOptions()
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});
app.UseAuthentication();
app.UseAuthorization();

app.Map("", (HttpContext context) =>
{
    if (!context.User.Identities.Any(x => x.IsAuthenticated))
        return Results.Redirect("account/login.html");
    return Results.Text(
"""
<div>hello world</div>
""", MediaTypeNames.Text.Html);
});
app.MapGet("p/{id}", async ([FromRoute] Guid id, [FromServices] FjsGramContext context) =>
{
    var post = await context.Posts.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == id);
    if (post is null)
        return Results.NotFound();
    return Results.Text(
$"""
<h1>{post.Title}</h1>
<h2>{post.Description}</h2>
{string.Join('\n', post.Images.Select(i => $"<img src=\"images/{i.Id}\"/>"))}
""", MediaTypeNames.Text.Html
);
});

app.AddAccountArea();

app.Run();
