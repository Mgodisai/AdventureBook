using AdventureBookApp.Enum;
using AdventureBookApp.Exception;
using AdventureBookApp.Model.Item;

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
        var itemToDrop = Inventory.GetAllItems().FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
        return itemToDrop != null ? DropItem(itemToDrop) : null;
    }

    public Item.Item? DropItem(Item.Item item)
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
    
    public void Consume(string consumableItem)
    {
        var itemToConsume = Inventory.GetAllItems().FirstOrDefault(item => item.Name.Equals(consumableItem, StringComparison.OrdinalIgnoreCase));
        if (itemToConsume is Consumable consumable)
        {
            Consume(consumable);
        }
    }

    private void Equip(Equipable equipable)
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

    public void Equip(string equipableItem)
    {
        var itemToEquip = Inventory.GetAllItems().FirstOrDefault(item => item.Name.Equals(equipableItem, StringComparison.OrdinalIgnoreCase));
        if (itemToEquip is Equipable equip)
        {
            Equip(equip);
        }
    }

    public void UnEquip()
    {
        if (EquippedItem == null) return;

        AdjustStats(EquippedItem, false);
        Inventory.AddItem(EquippedItem);
        EquippedItem = null;
    }

    private void AddToInventory(Item.Item item)
    {
        if (!Inventory.AddItem(item))
        {
            throw new InventoryOverloadException();
        }
    }

    private bool RemoveFromInventory(Item.Item item)
    {
        if (!Inventory.Contains(item)) return false;
        Inventory.RemoveItem(item);
        return true;
    }

    public string GetInventoryItems()
    {
        var equipped = EquippedItem is null ? "-" : EquippedItem.ToString();
        return $"Equipped: {equipped}, Inventory: {(Inventory.ToString() ?? "empty")}";
    }

    protected override void AdjustStats(Item.Item item, bool isPositive)
    {
        switch (item.Adjustment.PropertyType)
        {
            case PropertyType.Luck:
                switch (item.Adjustment.AdjustmentType)
                {
                    case AdjustmentType.Max: 
                        ActualLuck = _startingLuck;
                        break;
                    case AdjustmentType.Min:
                        ActualLuck = 0;
                        break;
                    case AdjustmentType.Reduce:
                    case AdjustmentType.Restore:
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

    public override string ToString()
    {
        var s = $", Luck: {ActualLuck}";
        return base.ToString()+s;
    }
}