using AdventureBookApp.Enum;
using AdventureBookApp.Model.Item;

namespace AdventureBookApp.Model.Entity;

public class Character : BaseEntity
{
    private readonly int _startingHealthPoint;
    private readonly int _startingSkillPoint;

    protected readonly IInventory<Item.Item> Inventory;
    
    public int ActualHealthPoint { get; private set; }
    public int ActualSkillPoint { get; private set; }
    protected Equipable? EquippedItem { get; set; }
    private CharacterType CharacterType { get; }
    private CreatureHealthStatus Status
    {
        get
        {
            var rate = ActualHealthPoint / (double)_startingHealthPoint;
            return rate switch
            {
                >= 0.9d => CreatureHealthStatus.Healthy,
                >= 0.5d => CreatureHealthStatus.LightlyWounded,
                >= 0.2d => CreatureHealthStatus.SeriouslyInjured,
                >= 0d => CreatureHealthStatus.CloseToDeath,
                _ => CreatureHealthStatus.Dead
            };
        }
    }

    protected Character(CharacterType characterType, string name, string description, int health,
        int skill, IInventory<Item.Item> startingInventory) : base(name, description)
    {
        Inventory = startingInventory;
        _startingHealthPoint = health;
        ActualHealthPoint = health;
        _startingSkillPoint = skill;
        CharacterType = characterType;
        ActualSkillPoint = skill;
    }

    public void TakeDamage(int damageAmount)
    {
        ActualHealthPoint = Math.Max(ActualHealthPoint - damageAmount, 0);
    }

    public void Heal(int healAmount, bool aboveMax)
    {
        ActualHealthPoint += aboveMax ? healAmount : Math.Min(_startingHealthPoint - ActualHealthPoint, healAmount);
    }
    
    protected virtual void AdjustStats(Item.Item item, bool isPositive)
    {
        switch (item.Adjustment.PropertyType)
        {
            case PropertyType.Skill:
                switch (item.Adjustment.AdjustmentType)
                {
                    case AdjustmentType.Max:
                        ActualSkillPoint = _startingSkillPoint;
                        break;
                    case AdjustmentType.Min:
                        ActualSkillPoint = 0;
                        break;
                    case AdjustmentType.Reduce:
                    case AdjustmentType.Restore:
                        ActualSkillPoint += item.Adjustment.Value * (isPositive ? 1 : -1);
                        break;
                }
                break;
            case PropertyType.Health:
                switch (item.Adjustment.AdjustmentType)
                {
                    case AdjustmentType.Max:
                        ActualHealthPoint = _startingHealthPoint;
                        break;
                    case AdjustmentType.Min:
                        ActualHealthPoint = 0;
                        break;
                    case AdjustmentType.Reduce:
                    case AdjustmentType.Restore:
                        ActualHealthPoint += item.Adjustment.Value * (isPositive ? 1 : -1);
                        break;
                }
                break;
        }
    }

    public override string ToString()
    {   
        return $"Type: {CharacterType}, Name: {Name}, Dex: {ActualSkillPoint}, HP: {ActualHealthPoint} ({Status})";
    }
}