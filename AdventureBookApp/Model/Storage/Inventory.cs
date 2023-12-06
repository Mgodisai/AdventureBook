namespace AdventureBookApp.Model;

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
        if (!CanAddItem(item)) return false;
        _storage.Add(item);
        return true;
    }

    public bool RemoveItem(Item.Item item) => _storage.Remove(item);
    public bool Contains(Item.Item item) => _storage.Contains(item);
    public bool CanAddItem(Item.Item item) => Capacity - CurrentLoad > item.Weight;

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
        return string.Join(',', _storage);
    }
}