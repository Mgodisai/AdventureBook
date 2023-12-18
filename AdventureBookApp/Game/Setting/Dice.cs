using AdventureBookApp.Enum;

namespace AdventureBookApp.Game.Setting;

public class Dice : IDice
{
    private readonly Random _random = new Random();
    private readonly DiceType _diceType;

    public Dice(DiceType diceType)
    {
        _diceType = diceType;
    }

    public int Roll()
    {
        return _random.Next(1, (int)_diceType + 1);
    }
}