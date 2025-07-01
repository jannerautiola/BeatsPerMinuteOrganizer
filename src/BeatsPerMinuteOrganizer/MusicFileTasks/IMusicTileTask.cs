namespace BeatsPerMinuteOrganizer.MusicFileTasks;

/// <summary>
/// Interface for tasks that operate on music files.
/// </summary>
public interface IMusicTileTask
{
    /// <summary>
    /// Executes the task on the provided music file.
    /// </summary>
    /// <param name="file"></param>
    void Execute(MusicFile file);
}