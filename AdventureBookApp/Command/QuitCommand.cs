using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class QuitCommand : ICommand
{
    public void Execute(GameContext context, string parameter)
    {
        context.ExitGame();
    }
    
    public string GetHelp()
    {
        return "Exits the game. Usage: quit or exit";
    }

}