using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.FileSystem;

/// <summary>
/// PrintOnlyFileSystem is a custom file system implementation that overrides the default file and directory operations
/// to support only printing file moves and directory creations without making any actual changes to the file system.
/// </summary>
public class PrintOnlyFileSystem : System.IO.Abstractions.FileSystem
{
    private readonly IDirectory _directory;

    private readonly IFile _file;

    /// <summary>
    /// Initializes a new instance of the <see cref="PrintOnlyFileSystem"/> class.
    /// </summary>
    public PrintOnlyFileSystem()
    {
        _directory = new PrintOnlyDirectory(this);
        _file = new PrintOnlyFile(this);
    }

    public override IDirectory Directory => _directory;
    public override IFile File => _file;
}