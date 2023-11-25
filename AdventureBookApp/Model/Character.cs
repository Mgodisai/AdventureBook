namespace AdventureBookApp.Model;

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