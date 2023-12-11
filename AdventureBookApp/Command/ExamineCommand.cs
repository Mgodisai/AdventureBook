using AdventureBookApp.Exception;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class ExamineCommand : ICommand
{
    public void Execute(GameContext gameContext, string entityName)
    {
        var itemInInventory = gameContext.Player.GetInventoryItems().FirstOrDefault(item =>
            item.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (itemInInventory != null)
        {
            Console.WriteLine(itemInInventory.Description);
            return;
        }
        
        var itemInSection = gameContext.CurrentSection?.GetItems().FirstOrDefault(item =>
            item.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (itemInSection != null)
        {
            Console.WriteLine(itemInSection.Description);
            return;
        }
        
        var characterInSection = gameContext.CurrentSection?.GetCharacters().FirstOrDefault(character =>
            character.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (characterInSection != null)
        {
            Console.WriteLine(characterInSection.Description);
            return;
        }

        ConsoleExtensions.WriteError("Entity not found.");
    }

    public string GetHelp()
    {
        return "Examines an entity for more information. Usage: examine [entityName]";
    }
}