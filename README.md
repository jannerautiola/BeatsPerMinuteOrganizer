# BeatsPerMinuteOrganizer

Console application to organize music files by beats per minute (BPM).

Uses ManagedBass and ManagedBass.Fx libraries for calculating BPM. These require bass.dll and bass_fx.dll which are included in the project.

## Usage and Options

`Usage: BeatsPerMinuteOrganizer -f <folder> [options]`

### Required Options
    -f, --folder <folder>             Folder to scan for music files.

### Moving Options
    -m, --move                        Whether to move files to their corresponding BPM folders. 
                                      Default is false.
      
        --move-bpm-gap <number>       If move option is true, this is the gap in which new folders are created. 
                                      Default is 20 (e.g., '60-79 BPM').
      
        --move-bpm-lower-bound <n>    If move option is true, this is the minimum BPM. All songs with lower BPM will 
                                      be moved to 'XX Or Less BPM' folder. 
                                      Default is 60.
      
        --move-bpm-upper-bound <n>    If move option is true, this is the maximum BPM. All songs with this or 
                                      greater BPM will be moved to 'XX Or Over BPM' folder. 
                                      Default is 240.
  
### Renaming Options
    -r, --rename                      Whether to rename files with BPM value at the end. 
                                      Default is false.

### General Options
    -e, --extension <extension>       File extension to scan. 
                                      Default is .mp3.

        --skip-end <seconds>          How many seconds to skip at the end of the song when calculating BPM. 
                                      Default is 15 seconds.
      
        --skip-start <seconds>        How many seconds to skip at the start of the song when calculating BPM. 
                                      Default is 30 seconds.

    - p, --print-only                 Whether to print only the results without performing any file operations. 
                                      Default is false.
  
    -h, --help                        Print help text and exit
      

## Examples

```powershell

# Calculate BPMs and print the values without moving or renaming:
BeatsPerMinuteOrganizer -f "C:\Music"

# Calculate BPMs and move and rename:
BeatsPerMinuteOrganizer -f ./songs -e .wav --move -r

# Calculate BPMs and move with other than default
BeatsPerMinuteOrganizer -f ./songs --move --move-bpm-gap 10 --move-bpm-lower-bound 50 --move-bpm-upper-bound 200
```

## Development

Example LaunchSettings.json file:
```json
{
  "profiles": {
    "BeatsPerMinuteOrganizer": {
      "commandName": "Project",
      "commandLineArgs": "--folder C:\\Music\\"
    }
  }
}
```