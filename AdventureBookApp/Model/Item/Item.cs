using AdventureBookApp.Model.Entity;

namespace AdventureBookApp.Model.Item;

public abstract class Item : BaseEntity, ITakeable
{
    public readonly int Id;
    public double Weight { get; }
    public Adjustment Adjustment { get; }

    protected Item(int id, string name, string description, double weight, Adjustment adjustment)
    : base(name, description)
    {
        Id = id;
        Weight = weight;
        Adjustment = adjustment;
    }

    public override string ToString()
    {
        return Name;
    }

    private bool Equals(Item other)
    {
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Item)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}