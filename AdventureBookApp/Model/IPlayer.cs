namespace AdventureBookApp.Model;

public interface IPlayer
{
    void PickUpItem(Item item);
    void Consume(Consumable consumable);
    void Hold(Weapon weapon);
    string ListInventory();
}