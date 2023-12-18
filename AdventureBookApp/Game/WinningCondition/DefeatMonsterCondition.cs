using AdventureBookApp.Model.Entity;

namespace AdventureBookApp.Game.WinningCondition;

public class DefeatMonsterCondition : IWinningCondition
{
    private readonly Monster _monster;

    public DefeatMonsterCondition(Monster monster)
    {
        _monster = monster;
    }

    public bool IsSatisfied(GameContext context)
    {
        return context.IsMonsterDefeated(_monster);
    }
    
    public override string ToString()
    {
        return $"Defeat {_monster.Name}";
    }
}