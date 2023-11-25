using AdventureBookApp.Model;

namespace AdventureBookApp;

internal static class Program
{
    private static void Main()
    {
        Console.WriteLine("Hello world!");
        var book = new Book("MyAdventure", new List<string> {"author1", "author2"}, "This is the summary");
    } 
}