# Loom Video Downloader C#

Loomdl is a simple C# console application **designed to work with Windows-native libraries** that allows you to download Loom videos using a URL. It supports two main commands: `help` and `download`. The application will fetch and download a video from Loom by parsing the video ID from the given URL.

## Features

- Download Loom videos with a simple command.
- Option to provide a custom filename for the downloaded video.

## Build

1. Clone the repository to your local machine:
   ```bash
   git clone https://github.com/rickd3ckard/loomdl.git
   cd loomdl 
    ```
2. Build the project using the .NET CLI:
   ```bash
    dotnet build
    ```
3. The output executable can be found in the bin\Debug\net8.0 folder.

## Install to PATH
To use the loomdl command from anywhere in your command prompt (CMD), you'll need to add the built executable to your system's PATH environment variable.
### Windows
1. Find the path to the loomdl.exe file, located in the bin\Debug\net8.0 folder.
2. Open the Start menu, search for Environment Variables, and click on Edit the system environment variables.
3. In the System Properties window, click on the Environment Variables button.
4. Under System variables, find and select the Path variable, then click Edit.
5. Add the folder path containing loomdl.exe to the list.
6. Click OK to save the changes.

## Usage
Once installed, you can use the following commands:

**1. Help Command**
To display available commands:
```bash
loomdl help
```
**2. Download Command**
To download a Loom video:
```bash
loomdl download <video-url> [filename]
```
`<video-url>`: The URL of the Loom video you want to download.

`[filename]`: (Optional) The name you want to give to the downloaded file. If not provided, the video ID will be used as the filename.

### Example
```bash
loomdl download https://www.loom.com/share/abcd1234xyz
```
