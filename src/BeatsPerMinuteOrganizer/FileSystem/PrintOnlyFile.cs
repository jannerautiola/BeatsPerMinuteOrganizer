using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.FileSystem;

/// <summary>
/// Overrides the file operations to support only printing file moves without actual file system changes.
/// </summary>
/// <param name="fileSystem"></param>
/// <remarks>Override other methods as needed to prevent actual file operations.</remarks>
public class PrintOnlyFile(IFileSystem fileSystem) : FileWrapper(fileSystem)
{
    public override void Move(string sourceFileName, string destFileName)
    {
    }
}