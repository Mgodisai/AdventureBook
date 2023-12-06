namespace AdventureBookApp.Model.Item;

public class Consumable : Item
{
    public Consumable(int id, string name, string description, double weight, Adjustment adjustment) 
        : base(id, name, description, weight, adjustment)
    {
    }
}