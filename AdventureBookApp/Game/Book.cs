using System.Text;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Game;

public class Book
{
    public string Title { get; }
    public IEnumerable<string> Authors { get; }
    public string Summary { get;  }

    public World World { get; }
    
    public IPlayer Player { get; }

    public Book(string title, IEnumerable<string> authors, string summary, World world, IPlayer player)
    {
        Title = title;
        Authors = authors;
        Summary = summary;
        World = world;
        Player = player;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Title: {Title}");
        sb.AppendLine($"Authors: {string.Join(", ", Authors)}");
        sb.AppendLine($"Summary: {Summary}");
        return sb.ToString();
    }
}