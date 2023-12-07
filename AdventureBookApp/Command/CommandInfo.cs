namespace AdventureBookApp.Command;

public class CommandInfo
{
    public ICommand Command { get; }
    public List<string> Aliases { get; }

    public CommandInfo(ICommand command, params string[] aliases)
    {
        Command = command;
        Aliases = aliases.ToList();
    }
}
