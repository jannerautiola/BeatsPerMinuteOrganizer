using BeatsPerMinuteOrganizer.MusicFileTasks;
using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Processes music files by calculating their BPM, renaming them, and moving them to appropriate folders based on their BPM.
/// </summary>
/// <param name="args"></param>
/// <param name="fileSystem"></param>
public class MusicFileProcessor(CommandLineArgs args, IFileSystem fileSystem)
{
    /// <summary>
    /// Processes the music files according to the specified command line arguments.
    /// </summary>
    public void Process()
    {
        // Initialize tasks
        List<IMusicTileTask> musicFileTasks = [];

        using var bpmCalculatorTask = new BpmCalculatorTask(fileSystem, args);
        musicFileTasks.Add(bpmCalculatorTask);

        if (args.Rename)
        {
            musicFileTasks.Add(new MusicFileRenamerTask(fileSystem));
        }

        if (args.Move)
        {
            musicFileTasks.Add(new MusicFileMoverTask(fileSystem, args));
        }

        // Get music files
        IEnumerable<MusicFile> musicFiles = GetMusicFiles();

        // Process music files
        foreach (var musicFile in musicFiles)
        {
            ConsoleHelper.WriteLine(musicFile.FileName, ConsoleColor.Cyan);

            foreach (var musicFileTask in musicFileTasks)
            {
                musicFileTask.Execute(musicFile);
            }

            ConsoleHelper.WriteLine("");
        }
    }

    private IEnumerable<MusicFile> GetMusicFiles()
    {
        var musicFileManager = new MusicFileLoader(fileSystem);
        var musicFiles = musicFileManager.LoadFromDirectory(args.Folder, args.Extension);
        if (!musicFiles.Any())
        {
            ConsoleHelper.WriteLine("No music files found in the specified folder.", ConsoleColor.Yellow);
        }

        return musicFiles;
    }
}