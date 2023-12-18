using System.Collections.Specialized;
using AdventureBookApp.Enum;
using AdventureBookApp.Exception;
using AdventureBookApp.Model.Item;
using AdventureBookApp.Model.Storage;

namespace AdventureBookApp.Model.Entity;

public class Player : Character, IPlayer
{
    private readonly int _startingLuck;
    public int ActualLuck { get; set; }

    public Player(CharacterType type, string name, string description, int health, int skill,
        int luck, IInventory<Item.Item> startingInventory)
        : base(type, name, description, health, skill, startingInventory)
    {
        _startingLuck = luck;
        ActualLuck = luck;
    }

    public void PickUpItem(Item.Item item) => AddToInventory(item);
    public Item.Item? DropItem(string itemName)
    {
        if (itemName == EquippedItem?.Name)
        {
            UnEquip();
        }
        var itemToDrop = Inventory.GetAllItems().FirstOrDefault(i => i.Name != null && i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return itemToDrop != null ? DropItem(itemToDrop) : null;
    }

    private Item.Item? DropItem(Item.Item item)
    {
        return RemoveFromInventory(item) ? item : null;
    }

    private void Consume(Item.Item consumable)
    {
        if (Inventory.Contains(consumable))
        {
            AdjustStats(consumable, true);
            RemoveFromInventory(consumable);
        }
        else
        {
            AdjustStats(consumable, true);
        }
    }
    
    public bool Consume(string consumableItem)
    {
        var itemToConsume = Inventory.GetAllItems().FirstOrDefault(item => item.Name != null && item.Name.Equals(consumableItem, StringComparison.OrdinalIgnoreCase));
        if (itemToConsume is Consumable consumable)
        {
            Consume(consumable);
            return true;
        }

        return false;
    }

    private void Equip(Equipment equipable)
    {
        if (Inventory.Contains(equipable))
        {
            if (EquippedItem != null)
            {
                UnEquip();
            }
            EquippedItem = equipable;
            AdjustStats(EquippedItem, true);
            Inventory.RemoveItem(equipable);
        }
        else
        {
            throw new InvalidOperationException("Item not in inventory.");
        }
    }

    public bool Equip(string equipableItem)
    {
        var itemToEquip = Inventory.GetAllItems().FirstOrDefault(item => item.Name != null && item.Name.Equals(equipableItem, StringComparison.OrdinalIgnoreCase));
        if (itemToEquip is Equipment equip)
        {
            Equip(equip);
            return true;
        }

        return false;
    }

    public bool UnEquip()
    {
        if (EquippedItem == null) return false;

        AdjustStats(EquippedItem, false);
        try
        {
            Inventory.AddItem(EquippedItem);
        }
        catch (InventoryOverloadException ex)
        {
            return false;
        }

        EquippedItem = null;
        return true;
    }

    private void AddToInventory(Item.Item item)
    {
        Inventory.AddItem(item);
    }

    private bool RemoveFromInventory(Item.Item item)
    {
        if (!Inventory.Contains(item)) return false;
        Inventory.RemoveItem(item);
        return true;
    }
    
    public bool IsInventoryContains(Item.Item item)
    {
        return Equals(EquippedItem, item) || Inventory.Contains(item);
    }

    public string GetInventoryItemsAsString()
    {
        var equipped = EquippedItem is null ? "none" : EquippedItem.ToString();
        return $"Equipped: {equipped}\nInventory: {(Inventory.ToString() ?? "empty")}";
    }

    protected override void AdjustStats(Item.Item item, bool isPositive)
    {
        switch (item.Adjustment.PropertyType)
        {
            case PropertyType.Luck:
                switch (item.Adjustment.AdjustmentType)
                {
                    case AdjustmentType.Restore: 
                        ActualLuck = _startingLuck;
                        break;
                    case AdjustmentType.Modify:
                        ActualLuck += item.Adjustment.Value * (isPositive ? 1 : -1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(item.Adjustment.PropertyType), "Invalid Property Type");
                }
                break;
            case PropertyType.Health:
            case PropertyType.Skill:
            default:
                base.AdjustStats(item, isPositive);
                break;
        }
    }

    public override string GetStatistics()
    {
        var s = $", Luck: {ActualLuck}";
        return base.GetStatistics()+s;
    }
}