using System.Text;
using AdventureBookApp.Command;
using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game.Setting;
using AdventureBookApp.Model;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Game;

public class GameContext
{
    public readonly Book Book;
    private Section? _currentSection;
    public IPlayer Player { get; set; }
    private bool IsRunning { get; set; }

    private readonly CommandHandler _commandHandler;
    private readonly List<Monster> _defeatedMonsters;


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
        Book = book;
        _currentSection = book.World[1];
        PreviousSection = book.World[1];
        _commandHandler = new CommandHandler();
        _defeatedMonsters = new List<Monster>();
    }
    
    
    private bool IsWinningConditionSatisfied()
    {
        return Book.World.WinningConditions.All(wc => wc.IsSatisfied(this));
    }
    
    private void HandleWinningCondition()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("--------------------WINNING--------------------");
        sb.AppendLine("Congratulations! You have satisfied all winning conditions so won the game!");
        HandleCommand("quest");
        sb.AppendLine("------------------------------------------------");
        ConsoleExtensions.WriteLineSuccess(sb.ToString());
    }
    
    private void HandleCombat()
    {
        do
        {
            var enemy = CurrentSection?.GetMonster(m => m.MonsterType == MonsterType.Enemy && m.ActualHealthPoint > 0);
            if (enemy == null) break;
            if (CurrentSection != null) StartCombat((Player)Player, enemy, CurrentSection);
        } while (true);
    }

    public void StartCombat(Player player, Monster monster, Section section)
    {
        var combatManager = new CombatManager(
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
            ConsoleExtensions.WriteLineError("GAME OVER!");
            HandleCommand("quit");
        }
        else if (monster.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteLineSuccess($"You killed {monster.Name}!");
            _defeatedMonsters.Add(monster);
            foreach (var monsterItem in monster.GetInventoryItems())
            {
                section.AddItem(monsterItem);
            }
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
            if (IsWinningConditionSatisfied())
            {
                HandleWinningCondition();
                break;
            }
            var command = ConsoleInputReader.ReadString("Enter command (or get help): ");
            HandleCommand(command);
            if (IsWinningConditionSatisfied())
            {
                HandleWinningCondition();
                break;
            }
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
        ConsoleExtensions.WriteLineTitle(Book.Title + $" ({string.Join(", ", Book.Authors)})");
        ConsoleExtensions.WriteLineInfo(Book.Summary);
        ConsoleExtensions.WriteLineWarning("Quests (winning conditions):");
        ConsoleExtensions.WriteLineError(Book.World.GetWinningConditionsAsString());
    }

    private void PrintLeaveMessage()
    {
        ConsoleExtensions.WriteLineSuccess("BYE");
    }
    
    public bool IsMonsterDefeated(Monster monster) => _defeatedMonsters.Contains(monster);

}