using AdventureBookApp.Common;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Loader;

namespace AdventureBookApp.Game;

public class BookSelector
{
    private readonly GameDataLoader _bookLoader = new();
    
    public List<string> GetValidBookFiles(string directoryPath)
    {
        var jsonFiles = Directory.GetFiles(directoryPath, "*.json");
        var validBookFiles = new List<string>();

        foreach (var file in jsonFiles)
        {
            var jsonContent = File.ReadAllText(file);
            if (_bookLoader.IsValidBookJson(jsonContent))
            {
                validBookFiles.Add(file);
            }
        }

        return validBookFiles;
    }
    
    public string? SelectBook(List<string> bookFiles)
    {
        Console.WriteLine("Available Books:");
        for (int i = 0; i < bookFiles.Count; i++)
        {
            ConsoleExtensions.WriteLineNormalMessage($"{i + 1}: {Path.GetFileNameWithoutExtension(bookFiles[i])}");
        }

        var x = ConsoleInputReader.ReadInt("Enter the number of the book you want to load: ");
        if (x > 0 && x <= bookFiles.Count)
        {
            return bookFiles[x - 1];
        }
        else
        {
            Console.WriteLine("Invalid selection.");
            return null;
        }
    }
}