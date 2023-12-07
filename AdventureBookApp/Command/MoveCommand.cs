using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class MoveCommand : ICommand
{
    public void Execute(GameContext gameContext, string target)
    {
        if (int.TryParse(target, out int exitId))
        {
            var exit = gameContext.CurrentSection.GetExitById(exitId);
            if (exit != null && !exit.IsHidden)
            {
                var nextSection = exit.DestinationSection;
                if (nextSection != null)
                {
                    gameContext.CurrentSection = nextSection;
                    Console.WriteLine($"Moved to {nextSection.Name}.");
                }
                else
                {
                    ConsoleExtensions.WriteError("Can't move in that direction.");
                }
            }
            else
            {
                ConsoleExtensions.WriteError("Invalid exit.");
            }
        }
        else
        {
            ConsoleExtensions.WriteError("Invalid command format for move.");
        }
    }
    
    public string GetHelp()
    {
        return "Move to a different section. Usage: move [sectionIndex]";
    }
}
