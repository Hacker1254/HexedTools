using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using CoreRuntime.Manager;
using HexedTools.ModulesLibs;
using UnityEngine;

namespace HexedTools.HookUtils;

public class Logs
{
    private static bool _firstCall;
    private static readonly StringBuilder _buffer = new();
    private static readonly object _bufferLock = new();
    public static string LogLocation { get; private set; } = PathDataInfo.RootFilePath + "\\Log.txt";
    static Logs() => CoroutineManager.RunCoroutine(FlushBufferCoroutine());
    public static Action<string> MessageCallBack = (str) =>
    {
        if (!_firstCall && MessageCallBack != null)
        {
            File.WriteAllText(LogLocation, "");
            _firstCall = true;
            Console.SetOut(new MethodCallTextWriter(MessageCallBack));
        }

        lock (_bufferLock)
            _buffer.Append(str);
    };
    private static IEnumerator FlushBufferCoroutine()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(0.1f);

            string content;
            lock (_bufferLock)
            {
                if (_buffer.Length == 0)
                    continue;
                content = _buffer.ToString();
                _buffer.Clear();
            }

            try { File.AppendAllText(LogLocation, content); } catch (Exception e) { Console.WriteLine($"ConsoleTool failed to write buffer\n{e}"); }
        }
    }
    private static string _lastEscape = "";
    public static void WriteOutLine(string input = "", ConsoleColor Color = ConsoleColor.White) => WriteOut(input + "\n", Color);
    public static void WriteOut(string input, ConsoleColor Color = ConsoleColor.White)
    {
        if (string.IsNullOrEmpty(input)) MessageCallBack("\n");
        int index = 0;
        MessageCallBack(ConvertHexToASCI(ConsoleColorToHex(Color))); // Write rest with the coloring

        foreach (Match match in LogReaderRegex.ColorTagRegex().Matches(input))
        {
            MessageCallBack(input[index..match.Index]); // Write text before the color tag
            index = match.Index + match.Length;

            string colorCode = match.Groups[1].Value;
            if (colorCode.StartsWith('#') && colorCode.Length == 7)
                MessageCallBack(ConvertHexToASCI(colorCode));
            else if (Enum.TryParse(colorCode, true, out ConsoleColor consoleColor))
                MessageCallBack(ConvertHexToASCI(ConsoleColorToHex(consoleColor)));

            MessageCallBack(match.Groups[2].Value); // Write text inside the color tag
            MessageCallBack(ConvertHexToASCI(ConsoleColorToHex(Color))); // Write rest with the coloring
        }

        MessageCallBack(ConvertHexToASCI(ConsoleColorToHex(Color))); // Write rest with the coloring

        MessageCallBack(input[index..]); // print with color ending
        MessageCallBack(ConvertHexToASCI("#FFFFFF")); // Set the default color
    }
    public static void Clear() => MessageCallBack("Clear");
    public static void Log(string? message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.DarkMagenta, string Name = "HexedTools")
    {
        if (message == null)
        {
            WriteOutLine();
            return;
        }

        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);
        WriteOut($"[{Name}] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "), flagColor);
        WriteOutLine(message, Color);
    }
    public static void Log(ConsoleColor Color, string message, string flag = "") => Log(message, flag, Color);
    public static void Warn(string? message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.Red, string Name = "HexedTools")
    {
        if (message == null)
        {
            WriteOutLine();
            return;
        }

        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);
        WriteOut($"[{Name}] ", flagColor);
        WriteOut("[Warn] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "), flagColor);
        WriteOutLine(message, Color);
    }
    public static string Debug(string? message = null, string Name = "HexedTools") => $"{DateTime.Now:HH:mm:ss.fff}[{Name}] [Debug] " + message;
    public static void Error(Exception e) => Error(null, e);
    public static void Error(string? message, Exception? Error = null, string Name = "HexedTools")
    {
        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);
        WriteOut($"[{Name}] ", ConsoleColor.Red);
        WriteOut(string.IsNullOrEmpty(message) ? "Unknown Error: " : message + "\n", ConsoleColor.White);
        if (Error != null) WriteOutLine($"{Error}", ConsoleColor.Red);
    }
    public static string ConsoleColorToHex(ConsoleColor color) => color switch
    {
        ConsoleColor.Black => "#000000",
        ConsoleColor.DarkBlue => "#000080",
        ConsoleColor.DarkGreen => "#008000",
        ConsoleColor.DarkCyan => "#008080",
        ConsoleColor.DarkRed => "#5F0000",
        ConsoleColor.DarkMagenta => "#870087",
        ConsoleColor.DarkYellow => "#808000",
        ConsoleColor.Gray => "#C0C0C0",
        ConsoleColor.DarkGray => "#808080",
        ConsoleColor.Blue => "#0000FF",
        ConsoleColor.Green => "#00FF00",
        ConsoleColor.Cyan => "#00FFFF",
        ConsoleColor.Red => "#FF0000",
        ConsoleColor.Magenta => "#FF00FF",
        ConsoleColor.Yellow => "#FFFF00",
        ConsoleColor.White => "#FFFFFF",
        _ => "#FFFFFF"
    };
    public static string ConvertHexToASCI(string hexColor)
    {
        if (hexColor.StartsWith('#') && hexColor.Length == 7)
        {
            int r = Convert.ToInt32(hexColor.Substring(1, 2), 16);
            int g = Convert.ToInt32(hexColor.Substring(3, 2), 16);
            int b = Convert.ToInt32(hexColor.Substring(5, 2), 16);
            var str = $"\u001b[38;2;{r};{g};{b}m";
            if (_lastEscape != str)
                return (_lastEscape = str);
            else return "";
        }
        return "";
    }
    private static ConsoleColor lastcolor;
    public static ConsoleColor GetRandomConsoleColor()
    {
        var ca = new System.Random().Next(1, 16);
        var color = (ConsoleColor)ca;
        if (color == lastcolor)
            color = (ConsoleColor)(Enum.GetValues(typeof(ConsoleColor)).GetValue(ca - 1) ?? ConsoleColor.DarkMagenta);
        if (color == ConsoleColor.Black || color == ConsoleColor.Red)
            return ConsoleColor.DarkMagenta;
        lastcolor = color;
        return color;
    }
}

public class MethodCallTextWriter(Action<string> outputMethod) : TextWriter
{
    public override Encoding Encoding => Encoding.Default;
    public override void Write(char value) => outputMethod(value.ToString()); 
    public override void Write(string? value) => outputMethod(value);
    public override void WriteLine(string? value) => outputMethod(value + Environment.NewLine);
}