namespace AdventureBookApp.Model;

public interface IInventory<T> where T : Item.Item
{
    double CurrentLoad { get; }
    double Capacity { get; }
    bool AddItem(T item);
    bool RemoveItem(T item);
    bool Contains(T item);
    bool CanAddItem(T item);
    IEnumerable<T> GetAllItems();
    T? TakeItem(T item);
}