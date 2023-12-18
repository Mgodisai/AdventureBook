using System.Text;
using AdventureBookApp.ExtensionMethods;
using AdventureBookApp.Game;

namespace AdventureBookApp.Command;

public class QuestCommand : ICommand
{
    private delegate void WritingMethod(string text);
    public void Execute(GameContext context, string parameter)
    {
        var sb = new StringBuilder();
        
        ConsoleExtensions.WriteLineInfo("--------------------QUESTS--------------------");
        ConsoleExtensions.WriteLineInfo("Current quest status:");
        foreach (var winningCondition in context.Book.World.WinningConditions)
        {
            var writingMethod = winningCondition.IsSatisfied(context) 
                ? (WritingMethod)ConsoleExtensions.WriteLineSuccess 
                : (WritingMethod)ConsoleExtensions.WriteLineError;
            writingMethod($"- {winningCondition}");
        }
        ConsoleExtensions.WriteLineInfo("-----------------------------------------------");
    }

    public string GetHelp()
    {
        return "Shows the current quest status. Usage: quest";
    }
}