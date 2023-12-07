using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class InventoryCommand : ICommand
{
    public void Execute(GameContext context, string parameter)
    {
        Console.WriteLine(context.Player.GetInventoryItems());
    }
    
    public string GetHelp()
    {
        return "List items can be found in inventory. Usage: inventory";
    }
}