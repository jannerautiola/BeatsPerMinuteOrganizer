using CommandLine;

namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Represents the command line arguments for the Beats Per Minute Organizer application.
/// </summary>
public class CommandLineArgs
{
    /// <summary>
    /// File extension to scan. Default is .mp3.
    /// </summary>
    [Option('e', "extension", Default = ".mp3", HelpText = "File extension to scan. Default is .mp3.")]
    public string Extension { get; set; } = "";

    /// <summary>
    /// Folder to scan for music files. This is required.
    /// </summary>
    [Option('f', "folder", Required = true, HelpText = "Folder to scan for music files.")]
    public string Folder { get; set; } = "";

    /// <summary>
    /// Whether to calculate BPM for files. Default is true.
    /// </summary>
    [Option('m', "move", Default = false, HelpText = "Whether to move files to their corresponding BPM folders. Default is false.")]
    public bool Move { get; set; }

    /// <summary>
    /// If move option is true, this is the gap in which new folders are created. Default is 20 which would result to folders like '60-79 BPM'.
    /// </summary>
    [Option("move-bpm-gap", Default = 20, HelpText = "If move option is true, this is the gap in which new folders are created. Default is 20 which would result to folders like '60-79 BPM'")]
    public int MoveGap { get; set; }

    /// <summary>
    /// If move option is true, this is the maximum bpm. All songs having lower BPM will be moved to 'XX Or Less BPM' folder. Default is 60.
    /// </summary>
    [Option("move-bpm-lower-bound", Default = 60, HelpText = "If move option is true, this is the maximum bpm. All songs having lower BPM will be moved to 'XX Or Less BPM' folder. Default is 60.")]
    public int MoveLowerBound { get; set; }

    /// <summary>
    /// If move option is true, this is the maximum bpm. All songs having this or greater BPM will be moved to 'XX Or Over BPM' folder. Default is 240.
    /// </summary>
    [Option("move-bpm-upper-bound", Default = 240, HelpText = "If move option is true, this is the maximum bpm. All songs having this or greater BPM will be moved to 'XX Or Over BPM' folder. Default is 240.")]
    public int MoveUpperBound { get; set; }

    /// <summary>
    /// Whether to print only the results without performing any file operations. Default is false.
    /// </summary>
    [Option('p', "print-only", Default = false, HelpText = "Whether to print only the results without performing any file operations. Default is false.")]
    public bool PrintOnly { get; set; }

    /// <summary>
    /// Whether to rename files with BPM value at the end. Default is false.
    /// </summary>
    [Option('r', "rename", Default = false, HelpText = "Whether to rename files with BPM value at the end. Default is false.")]
    public bool Rename { get; set; }

    /// <summary>
    /// How many seconds to skip at the end of the song when calculating BPM. This is useful for songs that have a long fade-out or silence at the end. Default is 15 seconds.
    /// </summary>
    [Option("skip-end", Default = 15, HelpText = "How many seconds to skip at the end of the song when calculating BPM. Default is 15 seconds.")]
    public int SkipSecondsEnd { get; set; }

    /// <summary>
    /// How many seconds to skip at the beginning of the song when calculating BPM. Default is 30 seconds.
    /// </summary>
    [Option("skip-start", Default = 30, HelpText = "How many seconds to skip at the start of the song when calculating BPM. Default is 30 seconds.")]
    public int SkipSecondsStart { get; set; }
}