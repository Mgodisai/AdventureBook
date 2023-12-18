using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game.Setting;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Storage;
using static AdventureBookApp.Game.Setting.GameRules;

namespace AdventureBookApp.Game.Preparation;

public static class PlayerGenerator
{
    public static IPlayer InitializePlayer()
    {
        var playerName = ConsoleInputReader.ReadString("Enter your character's name: ");
        var description = ConsoleInputReader.ReadString("Describe yourself: ");
        int initSkill, initHealth, initLuck;
        var currentNumberOfRolls = 0;
        Console.WriteLine($"Roll for points (max {MaxNumberOfRolls} tries)");
        do
        {
            currentNumberOfRolls++;
            ConsoleExtensions.WriteLineTitle($"Roll {currentNumberOfRolls}");
            
            initSkill = new Dice(DiceForSkill).Roll() + SkillBase;
            ConsoleExtensions.AnimateDiceRolling("Roll for skill points: ");
            ConsoleExtensions.WriteLineSuccess(initSkill.ToString());
            
            initHealth = new Dice(DiceForHealth).Roll() + HealthBase;
            ConsoleExtensions.AnimateDiceRolling("Roll for health points: ");
            ConsoleExtensions.WriteLineSuccess(initHealth.ToString());
            
            initLuck = new Dice(DiceForLuck).Roll() + LuckBase;
            ConsoleExtensions.AnimateDiceRolling("Roll for luck points: ");
            ConsoleExtensions.WriteLineSuccess(initLuck.ToString());
            
            if (currentNumberOfRolls == MaxNumberOfRolls)
            {
                break;
            }

        } while (ConsoleInputReader.ReadYesNo("Would you like to re-roll?"));

        Console.Write("Starting");
        for (var i = 0; i <= 5; i++)
        {
            Thread.Sleep(500);
            Console.Write(".");
            
        }
        return new Player(CharacterType.Human, playerName, description, initHealth, initSkill, initLuck, new Inventory(PlayerInventoryDefaultCapacity));
    }
}
