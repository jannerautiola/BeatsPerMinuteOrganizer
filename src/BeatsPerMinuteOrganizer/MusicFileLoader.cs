using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Class responsible for loading music files from a specified directory.
/// </summary>
/// <param name="fileSystem"></param>
public class MusicFileLoader(IFileSystem fileSystem)
{
    /// <summary>
    /// Loads music files from the specified directory with the given file extension.
    /// </summary>
    /// <param name="folder"></param>
    /// <param name="extension"></param>
    /// <returns></returns>
    public IEnumerable<MusicFile> LoadFromDirectory(string folder, string extension)
    {
        var files = fileSystem.Directory.GetFiles(folder, $"*{extension}");
        foreach (var file in files)
        {
            yield return new MusicFile
            {
                Folder = fileSystem.Path.GetDirectoryName(file)!,
                FileName = fileSystem.Path.GetFileName(file),
                Bpm = 0 // Bpm will be set later
            };
        }
    }
}