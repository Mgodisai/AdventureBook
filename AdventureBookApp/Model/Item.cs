namespace AdventureBookApp.Model;

public abstract class Item
{
    public Guid Id;
    public string Name { get; }
    public string Description { get; }
    public double Weight { get; }

    protected Item(Guid id, string name, string description, double weight)
    {
        Id = id;
        Name = name;
        Description = description;
        Weight = weight;
    }

    public abstract void Use(Character character);
}