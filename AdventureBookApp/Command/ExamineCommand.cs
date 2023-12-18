using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class ExamineCommand : ICommand
{
    public void Execute(GameContext gameContext, string entityName)
    {
        var itemInInventory = gameContext.Player.GetInventoryItems().FirstOrDefault(item =>
            item.Name != null && item.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (itemInInventory != null)
        {
            ConsoleExtensions.WriteLineInfo(itemInInventory.Description);
            return;
        }
        
        var itemInSection = gameContext.CurrentSection?.GetItems().FirstOrDefault(item =>
            item.Name != null && item.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (itemInSection != null)
        {
            ConsoleExtensions.WriteLineInfo(itemInSection.Description);
            return;
        }
        
        var characterInSection = gameContext.CurrentSection?.GetCharacters().FirstOrDefault(character =>
            character.Name != null && character.Name.Equals(entityName, StringComparison.OrdinalIgnoreCase));
        if (characterInSection != null)
        {
            ConsoleExtensions.WriteLineInfo(characterInSection.Description);
            return;
        }

        ConsoleExtensions.WriteLineError("Entity not found.");
    }

    public string GetHelp()
    {
        return "Examines an entity for more information. Usage: examine [entityName]";
    }
}