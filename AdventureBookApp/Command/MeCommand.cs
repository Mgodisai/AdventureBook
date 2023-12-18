using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class MeCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        ConsoleExtensions.WriteLineWarning(context.Player.GetStatistics() + "\n" + context.Player.Description);
        
    }
    
    public string GetHelp()
    {
        return "Print player's description. Usage: me";
    }
}
