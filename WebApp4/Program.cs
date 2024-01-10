using WebApp4;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var library = new Library();
var bookone = new BookBuilder();
var book = bookone
    .SetTitle("Name")
    .SetAuthor("AuthorName")
    .SetGenres(new[] { "Adventure" })
    .SetISBN("548754-23-1")
    .SetPublicationDate(new DateTime(1954, 7, 29))
    .SetAnnotation("Something")
    .SetTags(new[] { "classic" })
    .Build();
library.AddBook(book);

app.MapGet("/", () => "Hello!");
app.MapGet("/books", () => library.GetCatalog());
app.MapGet("/books/title/{title}", (string title) => library.SearchByTitle(title));
app.MapGet("/books/author/{author}", (string author) => library.SearchByAuthor(author));
app.MapGet("/books/isbn/{isbn}", (string isbn) => library.SearchByISBN(isbn));
app.MapGet("/books/tags/{tags}", (IEnumerable<string> tags) => library.SearchByTags(tags));
app.MapPost("/books", (Book book) => library.AddBook(book));

app.Run();
