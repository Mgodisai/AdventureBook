namespace AdventureBookApp.Model.Entity;

public abstract class BaseEntity
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }

    protected BaseEntity(string name, string description)
    {
        Id = Guid.NewGuid();
        Description = description;
        Name = name;
    }

    private bool Equals(BaseEntity other)
    {
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == this.GetType() && Equals((BaseEntity)obj);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }
}