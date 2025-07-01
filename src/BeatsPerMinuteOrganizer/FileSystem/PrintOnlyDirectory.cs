using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.FileSystem;

/// <summary>
/// Overrides the directory operations to support only printing directory creation without actual file system changes.
/// </summary>
/// <param name="fileSystem"></param>
/// <remarks>Override other methods as needed to prevent actual file operations.</remarks>
public class PrintOnlyDirectory(IFileSystem fileSystem) : DirectoryWrapper(fileSystem)
{
    private List<string> CreatedDirectories { get; } = [];

    public override IDirectoryInfo CreateDirectory(string path)
    {
        CreatedDirectories.Add(path);
        return default;
    }

    public override bool Exists(string path)
    {
        return CreatedDirectories.Any(d => d.Equals(path, StringComparison.OrdinalIgnoreCase));
    }
}