using AdventureBookApp.Common;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game.Setting;
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
        ConsoleExtensions.WriteLineError($"Battle started between {player.Name} and {monster.Name}");
        ConsoleExtensions.WriteLineInfo("------ BATTLE ------");
        while (player.ActualHealthPoint > 0 && monster.ActualHealthPoint > 0)
        {
            ConsoleExtensions.WriteLineTitle(player.GetStatistics());
            ConsoleExtensions.WriteLineWarning(monster.GetStatistics());
            var attackResult = PerformAttackRound(player, monster);
            
            if (CheckForDefeat(player, monster)) break;

            if (attackResult.Damage != 0)
            {
                if (AskForLuckTest())
                {
                    var isLucky = TestLuck(player);
                    ApplyLuckEffect(attackResult, isLucky);
                    if (CheckForDefeat(player, monster)) break;
                }
            }
            
            if (!AskForFlee()) continue;
            
            if (HandleFlee())
            {
                ConsoleExtensions.WriteLineSuccess("You have successfully fled the battle.");
                return true;
            }
            ConsoleExtensions.WriteLineError("You have failed to flee from the battle.");
        }
        ConsoleExtensions.WriteLineInfo("---- BATTLE ENDED ----");
        return false;
    }

    private AttackResult PerformAttackRound(Character attacker, Character defender)
    {
        var damage = _defaultDamage;
        var attackerRollValue = _diceForAttack.Roll();
        var attackerAttackStrength = attackerRollValue + attacker.ActualSkillPoint;
        ConsoleExtensions.AnimateDiceRolling(attacker.Name+" attack: ");
        ConsoleExtensions.WriteLineSuccess(attackerRollValue+"+"+attacker.ActualSkillPoint+ "="+attackerAttackStrength);
        
        var defenderRollValue = _diceForAttack.Roll();
        var defenderAttackStrength = defenderRollValue + defender.ActualSkillPoint;
        ConsoleExtensions.AnimateDiceRolling(defender.Name+" attack: ");
        ConsoleExtensions.WriteLineSuccess(defenderRollValue+"+"+defender.ActualSkillPoint+ "="+defenderAttackStrength);
        
        var isSuccessful = false;
        if (attackerAttackStrength > defenderAttackStrength)
        {
            defender.TakeDamage(damage);
            isSuccessful = true;
            ConsoleExtensions.WriteLineSuccess($"{attacker.Name} hits {defender.Name} for {_defaultDamage} damage.");
        }
        else if (defenderAttackStrength > attackerAttackStrength)
        {
            attacker.TakeDamage(damage);
            ConsoleExtensions.WriteLineError($"{defender.Name} hits {attacker.Name} for {_defaultDamage} damage.");
        }
        else
        {
            ConsoleExtensions.WriteLineWarning("The round is a draw.");
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
            ConsoleExtensions.WriteLineError("You have been defeated!");
            return true;
        }

        if (enemy.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteLineSuccess($"You have defeated the {enemy.Name}!");
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

        var roll = _diceForLuck.Roll();
        var result = roll <= player.ActualLuck;
        
        ConsoleExtensions.AnimateDiceRolling(player.Name+" luck test: ");
        ConsoleExtensions.WriteLineSuccess(roll+$" {(result?"<=":">")} "+player.ActualLuck);

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
                ConsoleExtensions.WriteLineSuccess($"{attackResult.Attacker.Name} is lucky and deals an additional {modifiedDamage} damage to {attackResult.Defender.Name}.");
            }
            else
            {
                attackResult.Defender.TakeDamage(-modifiedDamage);
                ConsoleExtensions.WriteLineError($"{attackResult.Attacker.Name} is unlucky and deals {modifiedDamage} less damage to {attackResult.Defender.Name}.");
            }
        }
        else
        {
            if (isLucky)
            {
                attackResult.Attacker.TakeDamage(-modifiedDamage);
                ConsoleExtensions.WriteLineError($"{attackResult.Attacker.Name} is lucky and reduces the damage received by {modifiedDamage}.");
            }
            else
            {
                attackResult.Attacker.TakeDamage(modifiedDamage);
                ConsoleExtensions.WriteLineError($"{attackResult.Attacker.Name} is unlucky and receives an additional {modifiedDamage} damage.");
            }
        }
    }

    private static bool AskForFlee()
    {
        return ConsoleInputReader.ReadYesNo("Do you want to flee?");
    }

    private bool HandleFlee()
    {
        var roll = _diceForFlee.Roll();
        var result = roll > _fleeThreshold;
        ConsoleExtensions.AnimateDiceRolling("Flee test: ");
        ConsoleExtensions.WriteLineSuccess(roll+$" {(result?">":"<=")} "+_fleeThreshold);
        return result;
    }
    
    private record AttackResult(Character Attacker, Character Defender, int Damage, bool IsSuccessful);
}