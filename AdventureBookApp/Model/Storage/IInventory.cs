namespace AdventureBookApp.Model.Storage;

public interface IInventory<T> where T : ITakeable
{
    double CurrentLoad { get; }
    double Capacity { get; }
    bool AddItem(T item);
    bool RemoveItem(T item);
    bool Contains(T item);
    IEnumerable<T> GetAllItems();
    T? TakeItem(T item);
}