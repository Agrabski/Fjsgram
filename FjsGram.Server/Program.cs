using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Net.Mime;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.AddServiceDefaults();

builder.Services.ConfigureHttpJsonOptions(options =>
{
});

var app = builder.Build();

app.MapDefaultEndpoints();
app.UseStaticFiles();

app.Map("", (HttpContext context) =>
{
    if (!context.User.Identities.Any(x=>x.IsAuthenticated))
        return Results.Redirect("/login.html");
        return Results.Text(
    """
<div>hello world</div>
""", MediaTypeNames.Text.Html);
});

app.MapPost("login", () => Results.Redirect("/"));

app.Run();
