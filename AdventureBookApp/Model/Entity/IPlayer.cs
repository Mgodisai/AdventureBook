namespace AdventureBookApp.Model.Entity;

public interface IPlayer
{
    void PickUpItem(Item.Item item);
    public Item.Item? DropItem(string item);
    bool Consume(string consumableItemName);
    bool Equip(string equipableItemName);
    bool UnEquip();
    string GetInventoryItems();
    string GetStatistics();
    string Description { get; }
}