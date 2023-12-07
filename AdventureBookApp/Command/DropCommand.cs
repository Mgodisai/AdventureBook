using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class DropCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        var droppedItem = context.Player.DropItem(itemName);
        if (droppedItem is not null)
        {
            context.CurrentSection?.AddItem(droppedItem);
            Console.WriteLine($"{itemName} has been dropped.");
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found.");
        }
    } 
    
    public string GetHelp()
    {
        return "Remove item from the inventory and drop at actualSection. Usage: drop [itemName]";
    }
}