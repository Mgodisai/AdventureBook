namespace AdventureBookApp.Model;

public class Weapon : Item
{
    private int Damage { get; }
    public Weapon(Guid id, string name, string description, double weight, int damage) 
        : base(id, name, description, weight)
    {
        Damage = damage;
    }

    public override void Use(Character character)
    {
        character.Health -= Damage;
    }
}