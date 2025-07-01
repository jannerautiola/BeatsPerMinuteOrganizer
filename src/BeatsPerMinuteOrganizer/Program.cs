using BeatsPerMinuteOrganizer.FileSystem;
using CommandLine;
using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Main entry point for the Beats Per Minute Organizer application.
/// </summary>
public class Program
{
    /// <summary>
    /// Main method that serves as the entry point for the application.
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        Parser.Default.ParseArguments<CommandLineArgs>(args)
            .WithParsed(RunApp)
            .WithNotParsed(HandleParseError);
    }

    private static void HandleParseError(IEnumerable<Error> errs)
    {
        // Handle errors in command line parsing
        foreach (var error in errs)
        {
            Console.WriteLine($"Error: {error.Tag}");
        }
    }

    private static void RunApp(CommandLineArgs args)
    {
        IFileSystem fileSystem;
        if (args.PrintOnly)
        {
            ConsoleHelper.WriteLine("Running in print-only mode. No file operations will be performed.", ConsoleColor.Yellow);
            fileSystem = new PrintOnlyFileSystem();
        }
        else
        {
            fileSystem = new System.IO.Abstractions.FileSystem();
        }

        var processor = new MusicFileProcessor(args, fileSystem);

        processor.Process();
    }
}