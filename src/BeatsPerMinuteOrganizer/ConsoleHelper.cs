namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Helper class for console output formatting.
/// </summary>
internal class ConsoleHelper
{
    /// <summary>
    /// Writes a message to the console with the specified color.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void Write(string message, ConsoleColor color = ConsoleColor.White)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = originalColor;
    }

    /// <summary>
    /// Writes a line to the console with the specified color.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="color"></param>
    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
}