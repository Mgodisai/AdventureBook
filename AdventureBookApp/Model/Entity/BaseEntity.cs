namespace AdventureBookApp.Model.Entity;

public abstract class BaseEntity
{
    private readonly string _description;
    public string Name { get; }
    public string Description => this + "\n" + _description;

    protected BaseEntity(string name, string description)
    {
        _description = description;
        Name = name;
    }
}