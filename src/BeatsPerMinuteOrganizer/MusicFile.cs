namespace BeatsPerMinuteOrganizer;

/// <summary>
/// Represents a music file with its BPM, filename, and folder.
/// </summary>
public class MusicFile
{
    /// <summary>
    /// Gets or sets the BPM (Beats Per Minute) of the music file.
    /// </summary>
    public int Bpm { get; set; } = 0;

    /// <summary>
    /// Gets or sets the name of the music file, including its extension.
    /// </summary>
    public string FileName { get; set; } = "";

    /// <summary>
    /// Gets or sets the folder where the music file is located.
    /// </summary>
    public string Folder { get; set; } = "";

    /// <summary>
    /// Returns a string representation of the music file, including its filename and BPM.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"{FileName} ({Bpm} BPM)";
    }
}