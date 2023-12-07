using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class EquipCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        try
        {
            if (context.Player.Equip(itemName))
            {
                Console.WriteLine($"{itemName} has been equipped.");
            }
            else
            {
                Console.WriteLine($"{itemName} cannot be equipped");
            }
        }
        catch (InvalidOperationException ex)
        {
            ConsoleExtensions.WriteError(ex.Message);
        }
        
    }
    
    public string GetHelp()
    {
        return "Equip an equipable item from the inventory. Usage: equip [itemName]";
    }
}
