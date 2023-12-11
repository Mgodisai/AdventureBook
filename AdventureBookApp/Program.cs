using AdventureBookApp.Game;
using static AdventureBookApp.Game.DefineWorld;

namespace AdventureBookApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        var world1 = InitializeWorld();
        var player = InitializePlayer();
        var book1 = new Book("The Lost Amulet of Zanar",
            new List<string> { "Author One", "Author Two" },
            "The player's quest is to recover the Lost Amulet of Zanar, an ancient artifact hidden deep within the Caves of Mystery. The amulet is said to possess the power to bring peace to the land.",
            world1);
        var gameContext = new GameContext(player, book1);
        
       gameContext.Run();
    }
}