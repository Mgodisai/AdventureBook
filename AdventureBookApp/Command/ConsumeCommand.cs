using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class ConsumeCommand : ICommand
{
    public void Execute(GameContext context, string itemName)
    {
        try
        {
            if (context.Player.Consume(itemName))
            {
                Console.WriteLine($"{itemName} has been eaten.");
            }
            else
            {
                Console.WriteLine($"{itemName} cannot be eaten");
            }
        }
        catch (InvalidOperationException ex)
        {
            ConsoleExtensions.WriteLineError(ex.Message);
        }
        
    }
    
    public string GetHelp()
    {
        return "Eat a consumable item from the inventory. Usage: eat [itemName]";
    }
}
