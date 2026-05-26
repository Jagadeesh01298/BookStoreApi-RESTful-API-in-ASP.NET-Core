using Microsoft.EntityFrameworkCore;
using BookStoreApi.Data;
using BookStoreApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add controllers and avoid JSON object cycle problem
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add In-Memory Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("BookStoreDb"));

var app = builder.Build();

// Enable Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Add sample data
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Authors.Any())
    {
        var author1 = new Author
        {
            Id = 1,
            Name = "J. K. Rowling",
            Country = "United Kingdom"
        };

        var author2 = new Author
        {
            Id = 2,
            Name = "Chetan Bhagat",
            Country = "India"
        };

        context.Authors.AddRange(author1, author2);

        context.Books.AddRange(
            new Book
            {
                Id = 1,
                Title = "Harry Potter and the Philosopher's Stone",
                PublicationYear = 1997,
                AuthorId = 1
            },
            new Book
            {
                Id = 2,
                Title = "Harry Potter and the Chamber of Secrets",
                PublicationYear = 1998,
                AuthorId = 1
            },
            new Book
            {
                Id = 3,
                Title = "Five Point Someone",
                PublicationYear = 2004,
                AuthorId = 2
            }
        );

        context.SaveChanges();
    }
}

app.Run();
