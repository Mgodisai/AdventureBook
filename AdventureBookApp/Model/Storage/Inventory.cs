using System.Text;
using AdventureBookApp.Exception;

namespace AdventureBookApp.Model.Storage;

public class Inventory : IInventory<Item.Item>
{
    public double Capacity { get; }
    public double CurrentLoad =>_storage.Sum(i => i.Weight);
    
    private readonly List<Item.Item> _storage;

    public Inventory(double maxCapacity)
    {
        Capacity = maxCapacity;
        _storage = new List<Item.Item>();
    }

    public bool AddItem(Item.Item item)
    {
        if (!CanAddItem(item))
        {
            throw new InventoryOverloadException($"Current capacity ({Capacity-CurrentLoad}) of the inventory is not enough to take: {item} ({item.Weight})");
        }
        _storage.Add(item);
        return true;
    }

    public bool RemoveItem(Item.Item item) => _storage.Remove(item);
    public bool Contains(Item.Item item) => _storage.Contains(item);
    private bool CanAddItem(Item.Item item) => Capacity - CurrentLoad > item.Weight;

    public IEnumerable<Item.Item> GetAllItems()
    {
        return _storage;
    }

    public Item.Item? TakeItem(Item.Item item)
    {
        return _storage.Find(i=>i.Id==item.Id);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Max capacity: {Capacity}, current load: {CurrentLoad}");
        sb.AppendLine("Items:");
        if (_storage.Count == 0)
        {
            sb.AppendLine("Empty");
        }
        else
        {
            foreach (var item in _storage)
            {
                sb.AppendLine("- " + item);
            } 
        }
        return sb.ToString();
    }
}