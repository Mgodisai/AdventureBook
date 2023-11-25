namespace AdventureBookApp.Model;

public class Inventory : IInventory<Item>
{
    private readonly List<Item> _storage;

    public Inventory(IEnumerable<Item> storage)
    {
        _storage = storage.ToList();
    }

    public int GetCapacity() => _storage.Count;

    public void AddItem(Item item) => _storage.Add(item);

    public void RemoveItem(Item item) => _storage.Remove(item);

    public bool Contains(Item item) => _storage.Contains(item);

    public override string ToString()
    {
        return string.Join(',', _storage);
    }
}