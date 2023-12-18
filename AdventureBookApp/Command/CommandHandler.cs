using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class CommandHandler
{
    private readonly Dictionary<string, ICommand> _commands;

    public CommandHandler()
    {
        ICommand helpCommand = new HelpCommand();
        var commandInfos = new List<CommandInfo>()
        {
            new(new PickupCommand(), "pickup", "p", "take"),
            new(new InventoryCommand(), "inventory", "i"),
            new(new DropCommand(), "drop", "d"),
            new(new EquipCommand(), "equip", "wear", "e"),
            new(new UnequipCommand(), "unequip", "u"),
            new(new MoveCommand(), "move", "go", "m"),
            new(new AttackCommand(), "attack", "a"),
            new(new ConsumeCommand(), "eat", "consume"),
            new(new MeCommand(), "me"),
            new(new ExamineCommand(), "examine", "ex", "x"),
            new(new LookCommand(), "look", "l"),
            new(new QuitCommand(), "quit", "exit"),
            new(new QuestCommand(), "quest"),
            new(helpCommand, "help", "h")
        };

        _commands = new Dictionary<string, ICommand>();

        foreach (var commandInfo in commandInfos)
        {
            foreach (var commandName in commandInfo.Aliases)
            {
                _commands[commandName] = commandInfo.Command;
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
            commandObject.Execute(context, parameter);
        }
        else
        {
            ConsoleExtensions.WriteLineError("Unknown command.");
        }
    }
}