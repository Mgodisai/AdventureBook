using AdventureBookApp.ExtensionMethods;

namespace AdventureBookApp.Common;

public static class ConsoleInputReader
{
    public static string ReadString(string message)
    {
        string? input;
        do
        {
            ConsoleExtensions.WriteGameMessage(message);
            input = Console.ReadLine();
        } while (string.IsNullOrWhiteSpace(input));

        return input;
    }

    public static int ReadInt(string message)
    {
        int result;
        do
        {
            string resultString = ReadString(message);
            if (!int.TryParse(resultString, out result))
            {
                ConsoleExtensions.WriteError("Invalid input. Please enter a valid number.");
            }
        } while (result == 0);

        return result;
    }
    
    public static bool ReadYesNo(string message)
    {
        string? input;
        do
        {
            ConsoleExtensions.WriteGameMessage(message + " (y/n): ");
            input = Console.ReadLine()?.Trim().ToLower();
        } while (input != "y" && input != "n");

        return input == "y";
    }

}
