using AdventureBookApp.Game;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Command;

public class UnequipCommand : ICommand
{
    public void Execute(GameContext context, string parameter)
    {
        if (context.Player.UnEquip())
        {
            Console.WriteLine($"{parameter} has been unequipped.");
        }
        else
        {
            Console.WriteLine($"{parameter} cannot be unequipped.");
        }
    }
    
    public string GetHelp()
    {
        return "Unequip the actual equipped item and put back into invetory. Usage: unequip";
    }
}
