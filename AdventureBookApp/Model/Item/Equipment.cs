namespace AdventureBookApp.Model.Item;

public class Equipment : Item
{
    public Equipment(string name, string description, double weight, Adjustment adjustment) 
        : base(name, description, weight, adjustment)
    {

    }
}