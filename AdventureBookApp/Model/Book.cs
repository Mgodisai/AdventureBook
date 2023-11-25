using System.Text.Json.Serialization;

namespace AdventureBookApp.Model;

public class Book
{
    public string Title { get; }
    public IEnumerable<string> Authors { get; }
    public string Summary { get;  }

    public Book(string title, IEnumerable<string> authors, string summary)
    {
        Title = title;
        Authors = authors;
        Summary = summary;
    }
}