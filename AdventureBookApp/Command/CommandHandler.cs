using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class CommandHandler
{
    private readonly Dictionary<string, CommandInfo> _commands;

    public CommandHandler()
    {
        ICommand helpCommand = new HelpCommand();
        List<CommandInfo> commandInfos = new List<CommandInfo>()
        {
            new CommandInfo(new PickupCommand(), "pickup", "p", "take"),
            new CommandInfo(new InventoryCommand(), "inventory", "i"),
            new CommandInfo(new DropCommand(), "drop", "d"),
            new CommandInfo(new EquipCommand(), "equip", "wear", "e"),
            new CommandInfo(new UnequipCommand(), "unequip", "u"),
            new CommandInfo(new MoveCommand(), "move", "go", "m"),
            new CommandInfo(new AttackCommand(), "attack", "a"),
            new CommandInfo(new ConsumeCommand(), "eat", "consume"),
            new CommandInfo(new MeCommand(), "me"),
            new CommandInfo(new ExamineCommand(), "examine", "ex"),
            new CommandInfo(new LookCommand(), "look", "l"),
            new CommandInfo(new QuitCommand(), "quit", "exit"),
            new CommandInfo(helpCommand, "help", "h")
        };

        _commands = new Dictionary<string, CommandInfo>();

        foreach (var commandInfo in commandInfos)
        {
            foreach (var commandName in commandInfo.Aliases)
            {
                _commands[commandName] = commandInfo;
            }
        }
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