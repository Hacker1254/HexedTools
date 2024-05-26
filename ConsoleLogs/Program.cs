using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace WorldLogs;


class Program {
    static long lastLength = 0;
    static readonly string filePath = "C:\\HexedTools\\Log.txt";
    static Process vrcWindow;

    // Import necessary functions from Kernel32.dll
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    // Grrr stfu i didn't spend anytime on this it doesn't matter
    static void Main(string[] args) {
        Console.Title = "ConsoleLogs - HEXED";
        Console.OutputEncoding = Encoding.Unicode;

        IntPtr hConsole = GetStdHandle(-11);
        GetConsoleMode(hConsole, out uint mode);
        mode |= 0x0004;  // Enable virtual terminal processing/ ascii escapes
        SetConsoleMode(hConsole, mode);

        while (!File.Exists(filePath))
            Thread.Sleep(100);

        FileSystemWatcher watcher = new() {
            Path = Path.GetDirectoryName(filePath),
            Filter = Path.GetFileName(filePath),
            NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size,
            EnableRaisingEvents = true,
        };

        watcher.Changed += (sender, e) => {
            Thread.Sleep(100);

            string newText = ReadNewTextFromFile(filePath);
            if (!string.IsNullOrEmpty(newText))
                Console.Write(newText);
        };

        while (true) {
            if (vrcWindow == null) {
                foreach (var proc in Process.GetProcesses())
                    if (proc.ProcessName == "VRChat") {
                        Console.Clear();
                        lastLength = 0L;
                        vrcWindow = proc;
                        Console.WriteLine($"Attached to {proc.Id}");
                    } 
            } else if (vrcWindow.HasExited) {
                lastLength = 0;
                vrcWindow = null;
            }
            Thread.Sleep(400);
        }
    }

    private static bool skip = true; // Skip first reset message 

    // Function to read the new text added to the file since the last check
    private static string ReadNewTextFromFile(string filePath) {
        string newText = "";
        try {
            long newLength = new FileInfo(filePath).Length;

            if (newLength > lastLength) {
                using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                fs.Seek(lastLength, SeekOrigin.Begin);

                byte[] buffer = new byte[newLength - lastLength];
                fs.Read(buffer, 0, buffer.Length);
                newText = System.Text.Encoding.Default.GetString(buffer);

                lastLength = newLength;
            }
        } catch (Exception ex) {
            Console.WriteLine("Error reading file: " + ex.Message);
        }

        if (newText.StartsWith("Clear")) { // Hopefully all logs should start with an escape for color
            Console.Clear();
            Console.Write(string.Concat(newText.Skip("Clear".Length)));
            lastLength = new FileInfo(filePath).Length;
            return "";
        }
        return newText;
    }
}