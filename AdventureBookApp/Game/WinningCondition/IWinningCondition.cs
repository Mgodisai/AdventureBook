namespace AdventureBookApp.Game.WinningCondition;

public interface IWinningCondition
{
    bool IsSatisfied(GameContext context);
}