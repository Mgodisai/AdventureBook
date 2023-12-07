namespace AdventureBookApp.Model.Item;

public class Equipment : Item
{
    public Equipment(int id, string name, string description, double weight, Adjustment adjustment) 
        : base(name, description, weight, adjustment)
    {

    }
}