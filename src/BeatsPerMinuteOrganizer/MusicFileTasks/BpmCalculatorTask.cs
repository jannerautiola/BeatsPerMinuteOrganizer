using ManagedBass;
using ManagedBass.Fx;
using System.IO.Abstractions;

namespace BeatsPerMinuteOrganizer.MusicFileTasks;

/// <summary>
/// Calculates the BPM (Beats Per Minute) for music files using the Bass library.
/// </summary>
public class BpmCalculatorTask : IMusicTileTask, IDisposable
{
    private readonly CommandLineArgs commandLineArgs;

    private readonly IFileSystem fileSystem;
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="BpmCalculatorTask"/> class.
    /// </summary>
    /// <param name="fileSystem"></param>
    /// <param name="commandLineArgs"></param>
    public BpmCalculatorTask(IFileSystem fileSystem, CommandLineArgs commandLineArgs)
    {
        Bass.Init();
        this.fileSystem = fileSystem;
        this.commandLineArgs = commandLineArgs;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Executes the BPM calculation task on the provided music file.
    /// </summary>
    /// <param name="file"></param>
    public void Execute(MusicFile file)
    {
        ObjectDisposedException.ThrowIf(disposedValue, typeof(BpmCalculatorTask));

        var fullPath = fileSystem.Path.Combine(file.Folder, file.FileName);
        double bpm = CalculateBPM(fullPath);
        if (bpm < 0)
        {
            ConsoleHelper.WriteLine($"BPM detection failed for {file}. Error: {Bass.LastError}", ConsoleColor.Red);
            return;
        }
        file.Bpm = (int)bpm;
        Print(file);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                Bass.Free();
            }

            disposedValue = true;
        }
    }

    private double CalculateBPM(string filePath)
    {
        int stream = Bass.CreateStream(filePath, 0, 0, BassFlags.Decode);
        if (stream == 0)
        {
            ConsoleHelper.WriteLine("Failed to load stream.", ConsoleColor.Red);
            return -1;
        }

        double bpm = 0;
        if (true)
        {
            bpm = CalculateBPMUsingAverageAlgorithm(stream);
        }
        else
        {
            bpm = CalculateBPMForWholeSong(stream);
        }
        Bass.StreamFree(stream);
        return bpm;
    }

    private double CalculateBPMForWholeSong(int stream)
    {
        // calculate bpm from whole song except the first and last seconds:
        var length = Bass.ChannelGetLength(stream);
        var sec = Bass.ChannelBytes2Seconds(stream, length);
        var bpm = BassFx.BPMDecodeGet(stream, commandLineArgs.SkipSecondsStart, sec - commandLineArgs.SkipSecondsEnd, 0, BassFlags.Default, null, nint.Zero);
        return bpm;
    }

    private double CalculateBPMUsingAverageAlgorithm(int stream)
    {
        // calculate song by taking 5s clips and averaging the bpm:

        var length = Bass.ChannelGetLength(stream);
        var lengthSeconds = Bass.ChannelBytes2Seconds(stream, length);
        lengthSeconds = lengthSeconds - commandLineArgs.SkipSecondsStart - commandLineArgs.SkipSecondsEnd;

        var clipCount = 10; // Number of clips
        var clipLength = Math.Min(5, lengthSeconds / clipCount); // Length of each clip in seconds

        List<double> bpms = new List<double>();
        for (int i = 0; i < clipCount; i++)
        {
            double start = commandLineArgs.SkipSecondsStart + i * lengthSeconds / clipCount;
            double end = start + clipLength;

            double clipBpm = BassFx.BPMDecodeGet(stream, start, end, 0, BassFlags.FXBpmMult2, null, nint.Zero);
            bpms.Add(clipBpm);
        }

        // Calculate the average BPM from the clips. Skip low and high values to avoid outliers:
        var bpm = bpms.Order().Skip((int)(clipCount * 0.3)).Take((int)(clipCount * 0.4)).Average();
        return bpm;
    }

    private void Print(MusicFile file)
    {
        ConsoleHelper.Write("BPM: ");
        ConsoleHelper.WriteLine(file.Bpm.ToString(), file.Bpm <= 0 ? ConsoleColor.Red : ConsoleColor.Green);
    }
}