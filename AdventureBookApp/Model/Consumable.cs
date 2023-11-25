namespace AdventureBookApp.Model;

public class Consumable : Item
{
    private int HealthRestore { get; }
    private bool IsConsumed { get; set; } 

    public Consumable(Guid id, string name, string description, double weight, int healthRestore) 
        : base(id, name, description, weight)
    {
        HealthRestore = healthRestore;
        IsConsumed = false;
    }

    public override void Use(Character character)
    {
        if (IsConsumed) return;
        character.Health += HealthRestore;
        IsConsumed = true;
    }
}