using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class HelpCommand : ICommand
{
    private Dictionary<string, ICommand>? _commands;

    public void SetCommandList(Dictionary<string, ICommand> commandList)
    {
        _commands = commandList;
    }
    
    public void Execute(GameContext gameContext, string parameter)
    {
        if (string.IsNullOrEmpty(parameter))
        {
            if (_commands == null) return;
            ConsoleExtensions.WriteLineInfo("List of available commands, use 'help [commandName]' for more information.");
            ConsoleExtensions.WriteLineGameMessage(string.Join(", ", _commands.Keys));
        }
        else
        {
            if (_commands != null && _commands.TryGetValue(parameter, out var command))
            {
                ConsoleExtensions.WriteLineGameMessage($"{parameter} - {command.GetHelp()}");
            }
            else
            {
                ConsoleExtensions.WriteLineGameMessage($"No help available for '{parameter}'");
            }
        }
    }

    public string GetHelp()
    {
        return "Provides help information for commands. Usage: help [commandName]";
    }
}
