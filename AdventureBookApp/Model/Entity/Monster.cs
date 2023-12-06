using AdventureBookApp.Enum;

namespace AdventureBookApp.Model.Entity;

public class Monster : Character
{
    public MonsterType MonsterType { get; private set; }

    public Monster(CharacterType characterType, MonsterType monsterType, string name, string description, int health, int skill, IInventory<Item.Item> startingInventory) 
        : base(characterType, name, description, health, skill, startingInventory)
    {
        MonsterType = monsterType;
    }
}