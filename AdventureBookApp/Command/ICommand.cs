using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public interface ICommand
{
    void Execute(GameContext context, string parameter);
    string GetHelp();
}