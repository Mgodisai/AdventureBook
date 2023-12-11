using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class LookCommand : ICommand
{
    public void Execute(GameContext context, string parameter)
    {
        Console.Clear();
        ConsoleExtensions.WriteWarning(context.Player.GetStatistics());
        ConsoleExtensions.WriteTitle(context.CurrentSection?.Name);
        Console.WriteLine(context.CurrentSection?.Description);
        ConsoleExtensions.WriteNormalMessage(context.CurrentSection?.GetSectionContent());
    }

    public string GetHelp()
    {
        return "Displays the current location and its description. Usage: look";
    }
}