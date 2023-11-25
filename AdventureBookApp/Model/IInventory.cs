namespace AdventureBookApp.Model;

public interface IInventory<in T> where T : class
{
    int GetCapacity();
    void AddItem(T item);
    void RemoveItem(T item);
    bool Contains(T item);
}