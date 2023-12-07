namespace AdventureBookApp.Model.Item;

public class Consumable : Item
{
    public Consumable(int id, string name, string description, double weight, Adjustment adjustment) 
        : base(name, description, weight, adjustment)
    {
    }
}