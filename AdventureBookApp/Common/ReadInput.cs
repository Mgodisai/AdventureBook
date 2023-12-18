using AdventureBookApp.ExtensionMethods;

namespace AdventureBookApp.Common;

public static class ConsoleInputReader
{
    public static string ReadString(string message)
    {
        string? input;
        do
        {
            ConsoleExtensions.WriteLineGameMessage(message);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }
    
    public static bool ReadYesNo(string message)
    {
        string? input;
        do
        {
            ConsoleExtensions.WriteLineGameMessage(message + " (y/n): ");
            input = Console.ReadLine()?.Trim().ToLower();
        } while (input != "y" && input != "n");

        return input == "y";
    }
    
    public static int ReadInt(string message)
    {
        int input;
        do
        {
            ConsoleExtensions.WriteLineGameMessage(message);
        } while (!int.TryParse(Console.ReadLine(), out input));

        return input;
    }

}
