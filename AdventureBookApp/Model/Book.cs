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
    public int Health { get; }
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
}

public interface IInventory<in T> where T : class
{
    void AddItem(T item);
    void RemoveItem(T item);
}

public class Inventory : IInventory<Item>
{
    public void AddItem(Item item)
    {
        throw new NotImplementedException();
    }

    public void RemoveItem(Item item)
    {
        throw new NotImplementedException();
    }
}