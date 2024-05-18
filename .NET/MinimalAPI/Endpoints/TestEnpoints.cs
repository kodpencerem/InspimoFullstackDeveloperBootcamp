namespace MinimalAPI.Endpoints;

public static class TestEnpoints
{
    public static void AddTestEnpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/test", (IConfiguration configuration) => "Hello World");

        app.MapGet("/test2", (int age) =>
        {
            return "Hello world " + age;//23:17 görüşelim
        });

        app.MapGet("/get", () => "This is a Get request");
        app.MapPost("/post", () => "This is a Post request");
        app.MapPut("/put", () => "This is a Put request");
        app.MapDelete("/delete", () => "This is a Delete request");

        app.MapGet("/return-result",
        // [Authorize]
        () =>
        {
            return Results.Ok(new { Message = "API is working..." });
        });
    }
}
