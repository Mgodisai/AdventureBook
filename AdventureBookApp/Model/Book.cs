using System.Text.Json;
using System.Text.Json.Serialization;

namespace AdventureBookApp.Model;

public class Book
{
    public string Title { get; }
    public IEnumerable<string> Authors { get; }
    public string Summary { get;  }
    
}

public class Section
{
    
}

public abstract class Character
{
    public IInventory<Item> Inventory { get; }
    public string Name { get; }
    public int Health { get; set; }
    public int Strength { get;  }
    public bool IsNpc { get; }

    protected Character(IInventory<Item> startingInventory, string name, int health, bool isNpc = true)
    {
        Inventory = startingInventory;
        Name = name;
        Health = health;
        IsNpc = isNpc;
    }
}

public interface IPlayer
{
    void PickUpItem(Item item);
}

public class Player : Character, IPlayer
{
    public void PickUpItem(Item item)
    {
        Inventory.AddItem(item);
    }

    public Player(IInventory<Item> startingInventory, string name, int health, bool isNpc = true) : base(startingInventory, name, health, isNpc)
    {
    }
}

public enum CreatureHealthStatus
{
    Healthy,
    
}

public enum NpcType
{
    Enemy,
    Trader,
    Friendly
}

public abstract class Item
{
    public Guid Id;
    public string Name { get; }
    public string Description { get; }
    public double Weight { get; }

    protected Item(Guid id, string name, string description, double weight)
    {
        Id = id;
        Name = name;
        Description = description;
        Weight = weight;
    }

    public abstract void Use(Character character);
}

public class Weapon : Item
{
    private int Damage { get; }
    public Weapon(Guid id, string name, string description, double weight, int damage) 
        : base(id, name, description, weight)
    {
        Damage = damage;
    }

    public override void Use(Character character)
    {
        character.Health -= Damage;
    }
}

public class Consumable : Item
{
    private int HealthRestore { get; }

    public Consumable(Guid id, string name, string description, double weight, int healthRestore) 
        : base(id, name, description, weight)
    {
        HealthRestore = healthRestore;
    }

    public override void Use(Character character)
    {
        character.Health += HealthRestore;
    }
}

public interface IInventory<in T> where T : class
{
    int GetCapacity();
    void AddItem(T item);
    void RemoveItem(T item);
}

public class Inventory : IInventory<Item>
{
    private readonly List<Item> _storage;

    public Inventory(IEnumerable<Item> storage)
    {
        _storage = storage.ToList();
    }

    public int GetCapacity()
    {
        return _storage.Count;
    }

    public void AddItem(Item item)
    {
        _storage.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _storage.Remove(item);
    }
}

public interface IRepository<out T> {
    IEnumerable<T> GetAllItems();
}

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

