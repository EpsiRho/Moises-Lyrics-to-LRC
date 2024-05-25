using MoisesLyricsToLRC;
using System.Text.Json;

namespace MoisesLyricsToLRC
{
    public static class MoisesLyricsToLRC
    {
        public static Dictionary<string, string> LaunchArgs = new Dictionary<string, string>();
        public static void Main(string[] args)
        {
            ParseArgs(args);

            string path = @"";
            string timing = "";
            bool useWordTiming = false;

            Console.WriteLine("[Moises Lyrics to LRC]");

            if (LaunchArgs.ContainsKey("perWord"))
            {
                useWordTiming = true;
            }
            else if (LaunchArgs.ContainsKey("perLine"))
            {
                useWordTiming = false;
            }
            else
            {
                Console.Write("Use per word timings? (y/n): ");
                timing = Console.ReadLine();
                timing = timing.ToLower();
                if (timing == "y" || timing == "yes" || timing == "use" || timing == "word")
                {
                    useWordTiming = true;
                }
            }

            if (LaunchArgs.ContainsKey("jsonPath"))
            {
                path = LaunchArgs["jsonPath"].Replace("\\", "/");
            }
            else
            {
                Console.Write("Path to download JSON: ");
                path = Console.ReadLine().Replace("\\", "/");
            }

            Console.WriteLine($"[+] Reading path {path}");

            string json = File.ReadAllText(path);
            MoisesLyricJson lyricJson = new MoisesLyricJson();
            try
            {
                lyricJson = JsonSerializer.Deserialize<MoisesLyricJson>(json);
            }
            catch (Exception)
            {
                Console.WriteLine($"[!] Couldn't read JSON, possibly invalid or missing?");
                return;
            }

            string line = "";
            List<string> lines = new List<string>();
            List<FileContent> lyricItems = new List<FileContent>();
            if (lyricJson.modified == null || lyricJson.modified.Count() == 0)
            {
                Console.WriteLine("[+] No modifications found, using AI predictions");
                lyricItems = lyricJson.fileContent;
            }
            else
            {
                Console.WriteLine("[+] Using modified lyrics set");
                lyricItems = lyricJson.modified.Select(x => new FileContent(x)).ToList();
            }

            int count = 0;
            Console.Write($"[+] Converting lines: {count}");
            foreach (var item in lyricItems)
            {
                string start = TimeSpan.FromSeconds(item.start).ToString("mm\\:ss\\.ff");
                string end = TimeSpan.FromSeconds(item.end).ToString("mm\\:ss\\.ff");
                if (item.text == "<SOL>")
                {
                    line = $"[{start}]";
                }
                else if (item.text == "<EOL>")
                {
                    line += $"\n[{start}]";
                    lines.Add(line);
                    line = "";
                }
                else
                {
                    if (useWordTiming)
                    {
                        line += $" <{start}> {item.text}";
                    }
                    else
                    {
                        line += $" {item.text}";
                    }
                }
                Console.CursorLeft = 0;
                count++;
                Console.Write($"[+] Converting lines: {count}");
            }

            Console.WriteLine($"");
            Console.WriteLine($"[+] Writing lines");
            string folder = path.Substring(0, path.LastIndexOf("/"));
            string export = $"{folder}/lyrics.lrc";
            File.WriteAllLines(export, lines);

            Console.WriteLine($"[!] File output: {export}");
        }
        private static void ParseArgs(string[] args)
        {
            LaunchArgs = new Dictionary<string, string>();

            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i].Replace("-", "").Replace("\"", "");
                if (!args[i].StartsWith("-"))
                {
                    try
                    {
                        LaunchArgs[LaunchArgs.Last().Key] += $" {arg}";
                    }
                    catch (Exception)
                    {

                    }
                    break;
                }

                if (i + 1 >= args.Length)
                {
                    LaunchArgs.Add(arg, "");
                    break;
                }


                if (args[i + 1].StartsWith("-"))
                {
                    LaunchArgs.Add(arg, "");
                }
                else
                {
                    string parameter = args[i + 1];
                    LaunchArgs.Add(arg, parameter);
                    i++;
                }
            }
        }
    }
}