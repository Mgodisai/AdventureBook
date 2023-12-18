using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class LookCommand : ICommand
{
    public void Execute(GameContext context, string parameter)
    {
        Console.Clear();
        ConsoleExtensions.WriteLineWarning(context.Player.GetStatistics());
        ConsoleExtensions.WriteLineTitle(context.CurrentSection?.Name);
        Console.WriteLine(context.CurrentSection?.Description);
        ConsoleExtensions.WriteLineNormalMessage(context.CurrentSection?.GetSectionContent());
    }

    public string GetHelp()
    {
        return "Displays the current location and its description. Usage: look";
    }
}