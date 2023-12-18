namespace AdventureBookApp.Model.Item;

public class Consumable : Item
{
    public Consumable(string name, string description, double weight, Adjustment adjustment) 
        : base(name, description, weight, adjustment)
    {
    }
}