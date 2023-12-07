using AdventureBookApp.Exception;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Command;

public class ExamineCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        var item = context.CurrentSection.RemoveItem(itemName);
        if (item != null)
        {
            try
            {
                context.Player.PickUpItem(item);
                Console.WriteLine($"{itemName} has been picked up.");
            }
            catch (InventoryOverloadException ex)
            {
                ConsoleExtensions.WriteError(ex.Message);
                context.CurrentSection.AddItem(item);
            }
        }
        else
        {
            Console.WriteLine($"Item '{itemName}' not found.");
        }
    }
    
    public string GetHelp()
    {
        return "Examine visible items. Usage: examine [itemName]";
    }
}