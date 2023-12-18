namespace AdventureBookApp.ExtensionMethods;

public static class ConsoleExtensions
{
    private static void WriteInColor(string message, ConsoleColor color, bool needNewLine)
    {
        Console.ForegroundColor = color;
        if (needNewLine)
            Console.WriteLine(message);
        else
            Console.Write(message);
        
        Console.ResetColor();
    }
    
    public static void WriteLineSuccess(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Green, true);
    }
    
    public static void WriteSuccess(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Green, false);
    }

    public static void WriteLineError(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Red, true);
    }
    
    public static void WriteError(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Red, false);
    }
    
    public static void WriteLineWarning(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.DarkYellow, true);
    }
    
    public static void WriteLineTitle(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Blue, true);
    }
    
    public static void WriteLineInfo(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Yellow, true);
    }
    
    public static void WriteInfo(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.Yellow, false);
    }
    
    public static void WriteLineGameMessage(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.DarkGray, true);
    }
    
    public static void WriteLineNormalMessage(string? message)
    {
        WriteInColor(message ?? string.Empty, ConsoleColor.White, true);
    }
    
    public static void AnimateDiceRolling(string message)
    {
        var random = new Random();
        WriteInfo(message);
        for (var i = 0; i < 20; i++)
        {
            Console.Write(random.Next(0,100).ToString("D2"));
            Thread.Sleep(50);
            Console.Write("\b\b");
        }
        Console.Write("  \b\b");
    }
}
