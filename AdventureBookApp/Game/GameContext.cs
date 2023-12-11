using AdventureBookApp.Command;
using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Game;

public class GameContext
{
    private readonly Book _book;
    private Section? _currentSection;
    public IPlayer Player { get; set; }
    private bool IsRunning { get; set; }

    private readonly CommandHandler _commandHandler;

    public Section? CurrentSection
    {
        get => _currentSection;
        set
        {
            if (Equals(value, _currentSection)) return;
            PreviousSection = _currentSection;
            _currentSection = value;
            HandleCommand("look");
        }
    }

    private Section? PreviousSection { get; set; }

    public GameContext(IPlayer player, Book book)
    {
        Player = player;
        _book = book;
        _currentSection = book.World[1];
        PreviousSection = null;
        _commandHandler = new CommandHandler();
    }

    private void HandleCombat()
    {
        var enemy = CurrentSection?.GetMonster(m => m is { MonsterType: MonsterType.Enemy });
        if (enemy != null)
        {
            if (CurrentSection != null) StartCombat((Player)Player, enemy, CurrentSection);
        }
    }

    public void StartCombat(Player player, Monster monster, Section section)
    {
        CombatManager combatManager = new CombatManager(
            diceForAttack: new Dice(GameRules.DiceForAttack),
            diceForLuck: new Dice(GameRules.DiceForTryLuck),
            diceForFlee: new Dice(GameRules.DiceForFlee),
            defaultDamage: GameRules.DefaultDamage,
            fleeThreshold: GameRules.FleeThreshold);

        if (combatManager.Fight(player, monster))
        {
            var sectionToFlee = PreviousSection ?? CurrentSection;
            HandleCommand("move " + sectionToFlee?.Index);
        }

        if (player.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteError("GAME OVER!");
            HandleCommand("quit");
        }
        else if (monster.ActualHealthPoint <= 0)
        {
            section.RemoveCharacter(monster);
        }
    }

    private void HandleCommand(string fullCommandString)
    {
        _commandHandler.HandleCommand(fullCommandString, this);
    }

    public void Run()
    {
        PrintWelcomeMessage();
        IsRunning = ConsoleInputReader.ReadYesNo("Would you like to start playing?");
        if (IsRunning)
        {
            HandleCommand("look");
        }

        while (IsRunning)
        {
            HandleCombat();
            var command = ConsoleInputReader.ReadString("Enter command (type 'exit' to quit): ");
            HandleCommand(command);
        }
        PrintLeaveMessage();
    }

    public void ExitGame()
    {
        IsRunning = false;
    }

    private void PrintWelcomeMessage()
    {
        Console.Clear();
        ConsoleExtensions.WriteTitle(_book.Title + $"{string.Join(", ", _book.Authors)}");
        ConsoleExtensions.WriteInfo(_book.Summary);
    }

    private void PrintLeaveMessage()
    {
        ConsoleExtensions.WriteSuccess("BYE");
    }

}