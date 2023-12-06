namespace AdventureBookApp.Model.Item;

public class Equipable : Item
{
    public Equipable(int id, string name, string description, double weight, Adjustment adjustment) 
        : base(id, name, description, weight, adjustment)
    {

    }
}