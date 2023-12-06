namespace AdventureBookApp.ExtensionMethods;

public static class ConsoleExtensions
{
    private static void WriteInColor(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    public static void WriteSuccess(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Green);
    }

    public static void WriteError(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Red);
    }
    
    public static void WriteWarning(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.DarkYellow);
    }
    
    public static void WriteTitle(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Blue);
    }
    
    public static void WriteGameMessage(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.DarkGray);
    }
    
    public static void WriteNormalMessage(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.White);
    }
}
