using AdventureBookApp.Common;
using AdventureBookApp.Enum;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;
using AdventureBookApp.Model;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;
using static AdventureBookApp.Game.DefineWorld;

namespace AdventureBookApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        var world = InitializeWorld();
        var player = InitializePlayer();

        var book = new Book("The Lost Amulet of Zanar", 
                             new List<string> { "Author One", "Author Two" }, 
                             "The player's quest is to recover the Lost Amulet of Zanar, an ancient artifact hidden deep within the Caves of Mystery. The amulet is said to possess the power to bring peace to the land.", 
                             world, 
                             player);

        var running = true;
        var currentSection = world[1];
        Section? previousSection = null;
        while (running)
        {
            if (currentSection != previousSection)
            {
                Console.Clear();
                previousSection = currentSection; // Update previousSection
                ConsoleExtensions.WriteWarning(player.ToString());
                ConsoleExtensions.WriteTitle(currentSection?.Name);
                Console.WriteLine(currentSection?.Description);
                ConsoleExtensions.WriteNormalMessage(currentSection?.GetSectionContent());

            }

            var enemy = currentSection?.GetMonster(m => m is { MonsterType: MonsterType.Enemy });
            if (enemy is not null)
            {
                if (currentSection != null) StartCombat((Player)player, enemy, currentSection);
            }

        
            var command = ConsoleInputReader.ReadString("Enter command (type 'exit' to quit): ");
            if (command == "exit")
            {
                running = false;
                continue;
            }

            ProcessCommand(command, player, ref currentSection);
        }
    }
    
    private static void StartCombat(Player player, Monster monster, Section section)
    {
        CombatManager combatManager = new CombatManager(new Dice(DiceType.D12), 2, 5);
        combatManager.Fight(player, monster);

        if (player.ActualHealthPoint <= 0)
        {
            ConsoleExtensions.WriteError("GAME OVER!");
        }
        else if (monster.ActualHealthPoint <= 0)
        {
            section.RemoveCharacter(monster); 
        }
    }
    

    private static void ProcessCommand(string fullCommandString, IPlayer player, ref Section? section)
    {
        var commandParts = fullCommandString.Split(' ');
        var command = commandParts[0].ToLower();
        var parameter = commandParts.Length > 1 ? string.Join(" ", commandParts.Skip(1)) : string.Empty;

        switch (command)
        {
            case "inventory":
            case "i":
                DisplayInventory(player);
                break;
            case "pickup":
            case "take":
            case "t":
                if (section != null) PickUpItem(parameter, player, section);
                break;
            case "drop":
            case "d":
                if (section != null) DropItem(parameter, player, section);
                break;
            case "equip":
            case "e":
                EquipItem(parameter, player);
                break;
            case "unequip":
                UnEquipItem(player);
                break;
            case "eat":
            case "consume":
                ConsumeItem(parameter, player);
                break;
            case "move":
            case "go":
            case "g":
                HandleMovement(parameter, ref section);
                break;
            case "me":
                ConsoleExtensions.WriteWarning(player.Description);
                break;
            case "examine":
            case "ex":
                ConsoleExtensions.WriteWarning(parameter);
                break;
            default:
                ConsoleExtensions.WriteError("Unknown command.");
                break;
        }
    }

    private static void DisplayInventory(IPlayer player)
    {
        Console.WriteLine(player.GetInventoryItems());
    }

    private static void PickUpItem(string itemName, IPlayer player, Section section)
    {
        var item = section.RemoveItem(itemName);
        if (item != null)
        {
            player.PickUpItem(item);
        }
    }

    private static void DropItem(string itemName, IPlayer player, Section section)
    {
        section.AddItem(player.DropItem(itemName));
    }

    private static void EquipItem(string itemName, IPlayer player)
    {
        
        player.Equip(itemName);
    }

    private static void UnEquipItem(IPlayer player)
    {
        player.UnEquip();
    }

    private static void ConsumeItem(string itemName, IPlayer player)
    {
        player.Consume(itemName);
    }

    private static void HandleMovement(string target, ref Section? section)
    {
        if (target is null)
        {
            return;
        }
        if (int.TryParse(target, out int exitId))
        {
            if (section != null)
            {
                var nextSection = MoveToSection(exitId, section);
                if (nextSection != null)
                {
                    section = nextSection;
                }
            }
        }
        else
        {
            ConsoleExtensions.WriteError("Invalid exit");
        }
    }

    private static Section? MoveToSection(int exitId, Section currentSection)
    {
        var exit = currentSection.GetExitById(exitId);
        if (exit != null && !exit.IsHidden)
        {
            var nextSection = exit.DestinationSection;
            Console.WriteLine($"Moved to {nextSection.Name}.");
            return nextSection;
        }
        else
        {
            ConsoleExtensions.WriteError("Invalid exit.");
        }

        return null;
    }
}