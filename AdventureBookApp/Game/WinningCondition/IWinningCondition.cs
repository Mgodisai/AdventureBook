namespace AdventureBookApp.Game;

public interface IWinningCondition
{
    bool IsSatisfied(GameContext context);
}