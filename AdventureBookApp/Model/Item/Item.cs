using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Storage;

namespace AdventureBookApp.Model.Item;

public abstract class Item : BaseEntity, ITakeable
{
    public double Weight { get; }
    public Adjustment Adjustment { get; }

    protected Item(string name, string description, double weight, Adjustment adjustment)
    : base(name, description)
    {
        Weight = weight;
        Adjustment = adjustment;
    }
}