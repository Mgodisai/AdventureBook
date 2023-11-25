namespace AdventureBookApp.Repository;

public interface IRepository<out T> {
    IEnumerable<T> GetAllItems();
}