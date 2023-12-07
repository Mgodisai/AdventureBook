using AdventureBookApp.Enum;
using AdventureBookApp.Model.Storage;

namespace AdventureBookApp.Model.Entity;

public class Monster : Character
{
    public MonsterType MonsterType { get; set; }

    public Monster(CharacterType characterType, MonsterType monsterType, string name, string description, int health, int skill, IInventory<Item.Item> startingInventory) 
        : base(characterType, name, description, health, skill, startingInventory)
    {
        MonsterType = monsterType;
    }

    public override string GetStatistics()
    {
        return base.GetStatistics() + $" Att: {MonsterType}";
    }
}