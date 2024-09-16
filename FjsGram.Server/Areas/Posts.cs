
using FjsGram.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

namespace FjsGram.Server.Areas;

public static class Posts
{
    public static WebApplication AddPostsArea(this WebApplication application)
    {
        var group = application
            .MapGroup("post")
        ;
        group.MapGet("explore", Explore);
        group.MapGet("{id:guid}", Post)

            ;
        return application;
    }

    private static async Task Post([FromRoute] Guid id, HttpContext context) => throw new NotImplementedException();
    private static async Task<IResult> Explore(HttpContext context, [FromServices] FjsGramContext db, CancellationToken token)
    {
        var posts = await db.Posts.OrderByDescending(x => x.Created).Take(24).ToArrayAsync(token);
        var chunks = posts.Chunk(3);
        return Results.Text(
$"""
<html>
    <head>
        <link rel="stylesheet" href="Post/ImageGrid.css"/>
    </head>
    <body>
    {string.Join('\n', chunks.Select(CreateRow))}
    </body>
</html>
""",
            MediaTypeNames.Text.Html
        );
    }

    private static string CreateRow(Post[] posts)
    {
        return
$"""
<div class="row">
</div>
""";
    }
}
