using Marten;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Weasel.Core;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(options =>
{
    // Establish the connection string to your Marten database
    options.Connection("Server=localhost;Port=65200;User Id=username;Password=password;Database=test");

    options.UseNewtonsoftForSerialization(
        EnumStorage.AsString,
        Casing.CamelCase,
        CollectionStorage.AsArray,
        NonPublicMembersStorage.All,
        configure => { configure.NullValueHandling = NullValueHandling.Ignore; });

    options.AutoCreateSchemaObjects = AutoCreate.All;
});

var app = builder.Build();

app.MapPost("/user",
    async ([FromServices] IDocumentStore store) =>
    {
        await using var session = store.LightweightSession();

        var user = new User
        {
            FirstName = "Test",
            LastName = "User",
            Internal = true
        };
        session.Store(user);

        await session.SaveChangesAsync();
    });

app.MapGet("/user",
    async ([FromServices] IDocumentStore store) =>
    {
        await using var session = store.QuerySession();

        var user = await session.Query<User>().FirstOrDefaultAsync();

        return Results.Ok(user);
    });

app.Run();
