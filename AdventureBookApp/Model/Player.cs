namespace AdventureBookApp.Model;

public class Player : Character, IPlayer
{

    public Player(IInventory<Item> startingInventory, string name, int health) 
        : base(startingInventory, name, health, false) {}
    
    public void PickUpItem(Item item) => Inventory.AddItem(item);

    public void Consume(Consumable consumable)
    {
        if (Inventory.Contains(consumable))
        {
            consumable.Use(this);
            Inventory.RemoveItem(consumable);
        }
        else
        {
            throw new NotImplementedException();
        }
    }

    public void Hold(Weapon weapon) => throw new NotImplementedException();

    public string ListInventory() => Inventory.ToString() ?? string.Empty;
}