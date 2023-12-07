using AdventureBookApp.Enum;

namespace AdventureBookApp.Game;

public static class GameRules
{
    public const int MaxNumberOfRolls = 3;
    
    public const DiceType DiceForSkill = DiceType.D6; 
    public const int SkillBase = 6;
    public const DiceType DiceForHealth = DiceType.D12;
    public const int HealthBase = 12;
    public const DiceType DiceForLuck = DiceType.D6;
    public const int LuckBase = 6;

    public const DiceType DiceForAttack = DiceType.D12;
    public const int DefaultDamage = 2;
    public const DiceType DiceForTryLuck = DiceType.D12;

    public const DiceType DiceForFlee = DiceType.D12;
    public const int FleeThreshold = 6;
}