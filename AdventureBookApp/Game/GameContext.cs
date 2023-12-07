using AdventureBookApp.Command;
using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Model;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Game;

public class GameContext
{
    private readonly Book _book;
    private Section? _currentSection;
    public IPlayer Player { get; set; }

    private readonly CommandHandler _commandHandler;

    public Section? CurrentSection
    {
        get => _currentSection;
        set
        {
            if (Equals(value, _currentSection)) return;
            PreviousSection = _currentSection;
            _currentSection = value;
            PrintState();
        }
    }
    private Section? PreviousSection { get; set; }

    // You can add other properties as needed, such as game time, inventory, etc.

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
        CombatManager combatManager = new CombatManager(new Dice(DiceType.D12), 2, 5);
        if (combatManager.Fight(player, monster))
        {
            Section? sectionToFlee = PreviousSection ?? CurrentSection;
            HandleCommand("move "+sectionToFlee?.Index);
        }

        if (player.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteError("GAME OVER!");
        } else if (monster.ActualHealthPoint <= 0)
        {
            section.RemoveCharacter(monster);
        }
    }

    private void PrintState()
    {
        Console.Clear();
        ConsoleExtensions.WriteWarning(Player.GetStatistics());
        ConsoleExtensions.WriteTitle(CurrentSection?.Name);
        Console.WriteLine(CurrentSection?.Description);
        ConsoleExtensions.WriteNormalMessage(CurrentSection?.GetSectionContent());
    }
    
    private bool PrintWelcome()
    {
        Console.Clear();
        ConsoleExtensions.WriteTitle(_book.Title + $"{string.Join(", ", _book.Authors)}");
        ConsoleExtensions.WriteInfo(_book.Summary);
        return ConsoleInputReader.ReadYesNo("Would you like to start playing?");
    }

    private void HandleCommand(string fullCommandString)
    {
        _commandHandler.HandleCommand(fullCommandString, this);
    }

    public void Start()
    {
        var running = PrintWelcome();
        if (running)
        {
            PrintState();
        }
        while (running)
        {
            HandleCombat();
            
            var command = ConsoleInputReader.ReadString("Enter command (type 'exit' to quit): ");
            if (command == "exit")
            {
                running = false;
                continue;
            }
            HandleCommand(command);
        }

        PrintByeMessage();
    }

    private void PrintByeMessage()
    {
        ConsoleExtensions.WriteSuccess("BYE");
    }
}