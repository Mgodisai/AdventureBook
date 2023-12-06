namespace AdventureBookApp.Model.Entity;

public interface IPlayer
{
    void PickUpItem(Item.Item item);
    public Item.Item? DropItem(string item);
    void Consume(string consumableItemName);
    void Equip(string equipableItemName);
    void UnEquip();
    string GetInventoryItems();
    string Description { get; }
}