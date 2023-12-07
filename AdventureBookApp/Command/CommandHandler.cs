using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class CommandHandler
{
    private readonly Dictionary<string, CommandInfo> _commands;

    public CommandHandler()
    {
        ICommand helpCommand = new HelpCommand();
        _commands = new Dictionary<string, CommandInfo>
        {
            { "pickup", new CommandInfo(new PickupCommand(), "pickup", "p", "take") },
            { "inventory", new CommandInfo(new InventoryCommand(), "inventory", "i") },
            { "drop", new CommandInfo(new DropCommand(), "drop", "d")},
            { "equip", new CommandInfo(new EquipCommand(), "equip", "wear", "e")},
            { "unequip", new CommandInfo(new UnequipCommand(), "unequip", "u")},
            { "move", new CommandInfo(new MoveCommand(), "move", "go", "m")},
            { "attack", new CommandInfo(new AttackCommand(), "attack", "a")},
            { "eat", new CommandInfo(new ConsumeCommand(), "eat", "consume")},
            { "me", new CommandInfo(new MeCommand(), "me")},
            { "examine", new CommandInfo(new ExamineCommand(), "examine", "ex")},
            { "help", new CommandInfo(helpCommand, "help", "h")},
        };
        
        ((HelpCommand)helpCommand).SetCommandList(_commands);
    }

    public void HandleCommand(string fullCommandString, GameContext context)
    {
        var commandParts = fullCommandString.Split(' ');
        var commandName = commandParts[0].ToLower();
        var parameter = commandParts.Length > 1 ? string.Join(" ", commandParts.Skip(1)) : string.Empty;
        
        if (_commands.TryGetValue(commandName, out var commandObject))
        {
            commandObject.Command.Execute(context, parameter);
        }
        else
        {
            ConsoleExtensions.WriteError("Unknown command.");
        }
    }
}
