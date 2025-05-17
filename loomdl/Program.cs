using System.Net;
using System.Text.Json.Nodes;

class Program
{
    static void Main(string[] Args)
    {
        if (Args.Length < 1)
        {
            Console.WriteLine("Unknown command. Type 'loomdl help' for available commands.");
            return;
        }

        string firstArgument = Args[0];

        switch (firstArgument)
        {
            case "help":
                DisplayHelp();
                return;

            case "download":
                string videoURI = Args[1];
                string videoName = string.Empty;

                if (Args.Length == 3) { videoName = Args[2]; }

                DownloadVideo(videoURI, videoName);
                return;

            default:
                Console.WriteLine("Unknown command. Type 'loomdl help' for available commands.");
                return;
        }
    }

    static void DownloadVideo(string VideoURI, string FileName = "")
    {
        string videoID = ExtractVideoID(VideoURI);
        string currentDir = Directory.GetCurrentDirectory();
        string fullFileName = string.IsNullOrWhiteSpace(FileName)
            ? Path.Combine(currentDir, videoID + ".mp4")
            : Path.Combine(currentDir, FileName + ".mp4");

        try
        {
            Console.WriteLine("Downloading video from: " + VideoURI);
            string videoDownloadURL = FetchLoomDownloadURL(videoID);

            DownloadLoomVideo(videoDownloadURL, fullFileName);

            Console.WriteLine("Download completed: " + fullFileName);

        }
        catch (Exception ex)
        {
            Console.WriteLine("An unexpected error occurred while downloading the video. The operation has been aborted:");
            Console.WriteLine(ex.Message);
        }
    }

    static string ExtractVideoID(string VideoURI)
    {
        Uri URI = new Uri(VideoURI);
        return URI.AbsolutePath.Split('/').Last();
    }

    static string FetchLoomDownloadURL(string VideoID)
    {
        string URL = $"https://www.loom.com/api/campaigns/sessions/{VideoID}/transcoded-url";
        WebRequest request = WebRequest.Create(URL);
        request.Method = "POST";

        using (WebResponse respose = request.GetResponse())
        {
            using (StreamReader reader = new StreamReader(respose.GetResponseStream()))
            {
                JsonObject content = JsonObject.Parse(reader.ReadToEnd()).AsObject();
                return (content["url"].ToString());
            };
        };
    }

    static void DownloadLoomVideo(string VideoURL, string FullFileName)
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(VideoURL, FullFileName);
        }
    }

    static void DisplayHelp()
    {
        Console.WriteLine("loomdl Help:");
        Console.WriteLine("  loomdl help                   - Shows this help message.");
        Console.WriteLine("  loomdl download <url> [name]  - Downloads a file from the specified URL.");
        Console.WriteLine("                        <url>   - The URL of the file to download.");
        Console.WriteLine("                        [name]  - (Optional) The name of the file. If not provided, the URI video ID is used.");
    }
}