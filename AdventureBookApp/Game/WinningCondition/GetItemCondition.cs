using AdventureBookApp.Model.Item;

namespace AdventureBookApp.Game.WinningCondition;

public class GetItemCondition : IWinningCondition
{
    private readonly Item _item;

    public GetItemCondition(Item item)
    {
        _item = item;
    }

    public bool IsSatisfied(GameContext context)
    {
        return context.Player.IsInventoryContains(_item);
    }
    
    public override string ToString()
    {
        return $"Get {_item.Name}";
    }
}