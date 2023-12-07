using AdventureBookApp.Common;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Model.Entity;

namespace AdventureBookApp.Game;

public class CombatManager
{
    private readonly IDice _diceForAttack;
    private readonly IDice _diceForLuck;
    private readonly IDice _diceForFlee;
    private readonly int _defaultDamage;
    private readonly int _fleeThreshold;
    

    public CombatManager(IDice diceForAttack, IDice diceForLuck, IDice diceForFlee, int defaultDamage, int fleeThreshold)
    {
        _defaultDamage = defaultDamage;
        _fleeThreshold = fleeThreshold;
        _diceForAttack = diceForAttack;
        _diceForLuck = diceForLuck;
        _diceForFlee = diceForFlee;
    }

    public bool Fight(Player player, Monster monster)
    {
        ConsoleExtensions.WriteError($"{player.Name} was attacked by {monster.Name}");
        ConsoleExtensions.WriteInfo("------ BATTLE ------");
        while (player.ActualHealthPoint > 0 && monster.ActualHealthPoint > 0)
        {
            ConsoleExtensions.WriteTitle(player.GetStatistics());
            ConsoleExtensions.WriteWarning(monster.GetStatistics());
            var attackResult = PerformAttackRound(player, monster);
            
            if (CheckForDefeat(player, monster)) break;

            if (attackResult.Damage != 0)
            {
                if (AskForLuckTest())
                {
                    var isLucky = TestLuck(player);
                    ApplyLuckEffect(attackResult, isLucky);
                }
            }
            
            if (!AskForFlee()) continue;
            
            if (HandleFlee())
            {
                ConsoleExtensions.WriteSuccess("You have successfully fled the battle.");
                return true;
            }
            ConsoleExtensions.WriteError("You have failed to flee from the battle.");
        }
        ConsoleExtensions.WriteInfo("---- BATTLE ENDED ----");
        return false;
    }

    private AttackResult PerformAttackRound(Character attacker, Character defender)
    {
        var damage = _defaultDamage;
        var attackerAttackStrength = RollForAttack(attacker.ActualSkillPoint);
        var defenderAttackStrength = RollForAttack(defender.ActualSkillPoint);
        var isSuccessful = false;
        if (attackerAttackStrength > defenderAttackStrength)
        {
            defender.TakeDamage(damage);
            isSuccessful = true;
            ConsoleExtensions.WriteSuccess($"{attacker.Name} hits {defender.Name} for {_defaultDamage} damage.");
        }
        else if (defenderAttackStrength > attackerAttackStrength)
        {
            attacker.TakeDamage(damage);
            ConsoleExtensions.WriteError($"{defender.Name} hits {attacker.Name} for {_defaultDamage} damage.");
        }
        else
        {
            ConsoleExtensions.WriteWarning("The round is a draw.");
            damage = 0;
        }

        return new AttackResult(
            Attacker: attacker,
            Defender: defender,
            Damage: damage,
            IsSuccessful: isSuccessful
        );
    }

    private static bool CheckForDefeat(Character player, Character enemy)
    {
        if (player.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteError("You have been defeated!");
            return true;
        }

        if (enemy.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteSuccess($"You have defeated the {enemy.Name}!");
            return true;
        }

        return false;
    }

    private static bool AskForLuckTest()
    {
        return ConsoleInputReader.ReadYesNo("Do you want to test your luck?");
    }

    private bool TestLuck(Player? player)
    {
        if (player is null)
        {
            throw new ArgumentNullException(paramName: nameof(player), message: "The parameter was null");
        }
        var result = _diceForLuck.Roll() <= player.ActualLuck;
        player.ActualLuck--;
        return result;
    }

    private static void ApplyLuckEffect(AttackResult attackResult, bool isLucky)
    {
        var modifiedDamage = attackResult.Damage / 2;
        if (attackResult.IsSuccessful)
        {
            if (isLucky)
            {
                attackResult.Defender.TakeDamage(modifiedDamage);
                ConsoleExtensions.WriteSuccess($"{attackResult.Attacker.Name} is lucky and deals an additional {modifiedDamage} damage to {attackResult.Defender.Name}.");
            }
            else
            {
                attackResult.Defender.TakeDamage(-modifiedDamage);
                ConsoleExtensions.WriteError($"{attackResult.Attacker.Name} is unlucky and deals {modifiedDamage} less damage to {attackResult.Defender.Name}.");
            }
        }
        else
        {
            if (isLucky)
            {
                attackResult.Attacker.TakeDamage(-modifiedDamage);
                ConsoleExtensions.WriteError($"{attackResult.Attacker.Name} is lucky and reduces the damage received by {modifiedDamage}.");
            }
            else
            {
                attackResult.Attacker.TakeDamage(modifiedDamage);
                ConsoleExtensions.WriteError($"{attackResult.Attacker.Name} is unlucky and receives an additional {modifiedDamage} damage.");
            }
        }
    }

    private static bool AskForFlee()
    {
        return ConsoleInputReader.ReadYesNo("Do you want to flee?");
    }

    private bool HandleFlee()
    {
        return _diceForFlee.Roll() > _fleeThreshold;
    }

    private int RollForAttack(int skill)
    {
        return _diceForAttack.Roll() + skill;
    }
    
    private record AttackResult(Character Attacker, Character Defender, int Damage, bool IsSuccessful);
}