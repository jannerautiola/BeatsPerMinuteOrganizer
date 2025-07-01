using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.MusicFileTasks;

/// <summary>
/// Moves music files to folders based on their BPM.
/// </summary>
/// <param name="fileSystem"></param>
/// <param name="commandLineArgs"></param>
public class MusicFileMoverTask(IFileSystem fileSystem, CommandLineArgs commandLineArgs) : IMusicTileTask
{
    /// <summary>
    /// Moves the provided music file to a folder based on its BPM.
    /// </summary>
    /// <param name="file"></param>
    public void Execute(MusicFile file)
    {
        var newFolder = GetFolderForFile(file);

        if (!fileSystem.Directory.Exists(newFolder))
        {
            ConsoleHelper.WriteLine($"Creating folder: {newFolder}", ConsoleColor.Magenta);
            fileSystem.Directory.CreateDirectory(newFolder);
        }
        MoveToFolder(file, newFolder);
        Print(file);
    }

    private string GetFolderForFile(MusicFile file)
    {
        // Use commandLineArgs.MoveGap, MoveMin, and MoveMax to determine the folder:
        int bpm = file.Bpm;

        var lowerBound = commandLineArgs.MoveLowerBound;
        var upperBound = commandLineArgs.MoveUpperBound;
        var gap = commandLineArgs.MoveGap;

        if (bpm < lowerBound)
        {
            return fileSystem.Path.Combine(file.Folder, $"{lowerBound} Or Less BPM");
        }
        else if (bpm >= upperBound)
        {
            return fileSystem.Path.Combine(file.Folder, $"{upperBound} Or Over BPM");
        }
        else
        {
            int lower = (bpm - lowerBound) / gap * gap + lowerBound;
            int upper = Math.Min(lower + gap - 1, upperBound);
            return fileSystem.Path.Combine(file.Folder, $"{lower}-{upper} BPM");
        }
    }

    private void MoveToFolder(MusicFile file, string destinationFolder)
    {
        string destinationPath = fileSystem.Path.Combine(destinationFolder, file.FileName);
        var fullPath = fileSystem.Path.Combine(file.Folder, file.FileName);
        fileSystem.File.Move(fullPath, destinationPath);
        file.Folder = destinationFolder;
    }

    private void Print(MusicFile file)
    {
        ConsoleHelper.Write("Moved to ");
        ConsoleHelper.WriteLine(file.Folder, ConsoleColor.Yellow);
    }
}