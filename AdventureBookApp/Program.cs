using AdventureBookApp.Game;
using AdventureBookApp.Game.Preparation;
using AdventureBookApp.Loader;

namespace AdventureBookApp;

internal static class Program
{
    public static void Main(string[] args)
    {
        var bookSelector = new BookSelector();
        var bookFiles = bookSelector.GetValidBookFiles(Directory.GetCurrentDirectory());

        var selectedBookFile = bookSelector.SelectBook(bookFiles);
        if (!string.IsNullOrEmpty(selectedBookFile))
        {
            var gameDataLoader = new GameDataLoader();
            var book = gameDataLoader.LoadBook(selectedBookFile);
            var player = PlayerGenerator.InitializePlayer();
            var gameContext = new GameContext(player, book);
            gameContext.Run();
            Console.ReadKey();
        }
    }
}