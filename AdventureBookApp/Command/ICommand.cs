using AdventureBookApp.Game;
using AdventureBookApp.Model.Entity;
using AdventureBookApp.Model.Location;

namespace AdventureBookApp.Command;

public interface ICommand
{
    void Execute(GameContext context, string parameter);
    string GetHelp();
}