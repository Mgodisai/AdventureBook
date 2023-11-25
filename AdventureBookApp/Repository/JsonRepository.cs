using System.Text.Json;
using AdventureBookApp.Model;

namespace AdventureBookApp.Repository;

public class JsonRepository : IRepository<Item>
{
    private readonly string _jsonFilePath;

    public JsonRepository(string filePath)
    {
        _jsonFilePath = filePath;
    }

    public IEnumerable<Item> GetAllItems()
    {
        List<Item>? items = null;
        try
        {
            var json = File.ReadAllText(_jsonFilePath);
            items = JsonSerializer.Deserialize<List<Item>>(json);
        }
        catch (FileNotFoundException e)
        {
            Console.WriteLine("File not found: " + e.Message);
        }
        catch (JsonException e)
        {
            Console.WriteLine("JSON parsing error: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred: " + e.Message);
        }
        return items ?? new List<Item>();
    }
}