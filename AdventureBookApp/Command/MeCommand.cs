using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Command;

public class MeCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        ConsoleExtensions.WriteWarning(context.Player.GetStatistics() + "\n" + context.Player.Description);
        
    }
    
    public string GetHelp()
    {
        return "Print player's description. Usage: me";
    }
}
