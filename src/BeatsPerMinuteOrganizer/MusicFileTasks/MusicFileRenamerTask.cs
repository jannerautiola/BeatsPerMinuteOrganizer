using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.MusicFileTasks;

/// <summary>
/// Renames music files to include their BPM in the filename.
/// </summary>
/// <param name="fileSystem"></param>
public class MusicFileRenamerTask(IFileSystem fileSystem) : IMusicTileTask
{
    /// <summary>
    /// Renames the provided music file to include its BPM in the filename.
    /// </summary>
    /// <param name="file"></param>
    public void Execute(MusicFile file)
    {
        string oldName = file.FileName;
        string newName = GetNewName(file);
        Rename(file, newName);
        Print(oldName, file);
    }

    private string GetNewName(MusicFile file)
    {
        var nameWithoutExtension = fileSystem.Path.GetFileNameWithoutExtension(file.FileName);
        var extension = fileSystem.Path.GetExtension(file.FileName);
        return $"{nameWithoutExtension} ({file.Bpm}){extension}";
    }

    private void Print(string oldName, MusicFile file)
    {
        ConsoleHelper.Write("Renamed to ");
        ConsoleHelper.WriteLine(file.FileName, ConsoleColor.DarkCyan);
    }

    private void Rename(MusicFile file, string newName)
    {
        string newFilePath = fileSystem.Path.Combine(file.Folder, newName);
        var fullPath = fileSystem.Path.Combine(file.Folder, file.FileName);
        fileSystem.File.Move(fullPath, newFilePath);
        file.FileName = newName;
    }
}