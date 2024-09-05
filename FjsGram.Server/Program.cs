using FjsGram.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureHttpJsonOptions(options =>
{
});
builder.AddSqlServerDbContext<FjsGramContext>("primary");
var app = builder.Build();

app.MapDefaultEndpoints();
app.UseStaticFiles();

app.Map("", (HttpContext context) =>
{
    if (!context.User.Identities.Any(x => x.IsAuthenticated))
        return Results.Redirect("/login.html");
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

app.MapPost("login", () => Results.Redirect("/"));

app.Run();
