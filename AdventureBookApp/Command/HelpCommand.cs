using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class HelpCommand : ICommand
{
    private Dictionary<string, CommandInfo>? _commands;

    public void SetCommandList(Dictionary<string, CommandInfo> commandList)
    {
        _commands = commandList;
    }
    
    public void Execute(GameContext gameContext, string parameter)
    {
        if (string.IsNullOrEmpty(parameter))
        {
            if (_commands != null)
                foreach (var command in _commands)
                {
                    Console.WriteLine($"{command.Key} ({string.Join(',', command.Value.Aliases)})");
                }
        }
        else
        {
            if (_commands != null && _commands.TryGetValue(parameter, out var command))
            {
                Console.WriteLine($"{parameter} - {command.Command.GetHelp()}");
            }
            else
            {
                Console.WriteLine($"No help available for '{parameter}'");
            }
        }
    }

    public string GetHelp()
    {
        return "Provides help information for commands. Usage: help [commandName]";
    }
}
