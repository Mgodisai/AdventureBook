using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class MoveCommand : ICommand
{
    public void Execute(GameContext gameContext, string target)
    {
        if (int.TryParse(target, out int exitId))
        {
            var exit = gameContext.CurrentSection?.GetExitById(exitId);
            if (exit != null && !exit.IsHidden)
            {
                var nextSection = exit.DestinationSection;
                gameContext.CurrentSection = nextSection;
                Console.WriteLine($"Moved to {nextSection.Name}.");
            }
            else
            {
                ConsoleExtensions.WriteLineError("Invalid exit.");
            }
        }
        else
        {
            ConsoleExtensions.WriteLineError("Invalid command format for move.");
        }
    }
    
    public string GetHelp()
    {
        return "Move to a different section. Usage: move [sectionIndex]";
    }
}
