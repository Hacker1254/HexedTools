using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using HexedTools.ModulesLibs;

namespace HexedTools.HookUtils; // im lazy shut tf up

public class Logs {
    static bool _firstCall;

    public static string LogLocation { get; private set; } = PathDataInfo.RootFilePath + "\\Log.txt";
    public static Action<string> MessageCallBack = new((str) => {
        if (!_firstCall) {
            File.WriteAllText(LogLocation, "");
            _firstCall = true;
            Console.SetOut(new MethodCallTextWriter(MessageCallBack));
        }
        try {
            File.AppendAllText(LogLocation, str);
        } catch (Exception e) {
            Console.WriteLine($"Failed to write {str}\n{e}");
        }
    });

    private static string _lastEscape = "";

    public static void WriteOutLine(string input = "", ConsoleColor Color = ConsoleColor.White) => WriteOut(input + "\n", Color);

    public static void WriteOut(string input, ConsoleColor Color = ConsoleColor.White) {
		if (string.IsNullOrEmpty(input)) MessageCallBack("\n");
        int index = 0;
        MessageCallBack(ConvertHexToAsci(ConsoleColorToHex(Color))); // Write rest with the coloring

        foreach (Match match in Regex.Matches(input, @"<color=(.*?)>(.*?)<\/color>")) {
            MessageCallBack(input.Substring(index, match.Index - index)); // Write text before the color tag
            index = match.Index + match.Length;

            string colorCode = match.Groups[1].Value;
            if (colorCode.StartsWith("#") && colorCode.Length == 7)
                MessageCallBack(ConvertHexToAsci(colorCode));
            else if (Enum.TryParse(colorCode, true, out ConsoleColor consoleColor))
                MessageCallBack(ConvertHexToAsci(ConsoleColorToHex(consoleColor)));

            MessageCallBack(match.Groups[2].Value); // Write text inside the color tag
            MessageCallBack(ConvertHexToAsci(ConsoleColorToHex(Color))); // Write rest with the coloring
        }

        MessageCallBack(ConvertHexToAsci(ConsoleColorToHex(Color))); // Write rest with the coloring

        MessageCallBack(input.Substring(index)); // print with color endding
        MessageCallBack(ConvertHexToAsci("#FFFFFF")); // Set the default color
    }

    public static void Clear() => MessageCallBack("Clear");

    public static void Log(string message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.DarkMagenta, string Name = "HexedTools") {
		if (message == null) {
            WriteOutLine();
			return;
		}

        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);

        WriteOut($"[{Name}] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "), flagColor);
		WriteOutLine(message, Color);
	}

    // Just Here to make things Faster
    public static void Log(ConsoleColor Color, string message, string flag = "") =>
        Log(message, flag, Color);

    public static void Warn(string message = null, string flag = "", ConsoleColor Color = ConsoleColor.White, ConsoleColor flagColor = ConsoleColor.Red, string Name = "HexedTools") {
		if (message == null) {
			WriteOutLine();
			return;
		}

        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);

        WriteOut($"[{Name}] ", flagColor);
		WriteOut("[Warn] " + (string.IsNullOrEmpty(flag) ? "" : $"[{flag}] "), flagColor);
        WriteOutLine(message, Color);
	}

	public static string Debug(string message = null, ConsoleColor Color = ConsoleColor.White, string Name = "HexedTools", string flag = "") {
		if (false) {
			if (message == null) {
				WriteOutLine();
				return null;
			}
			WriteOut($"[{Name}] ", ConsoleColor.DarkGray);
			WriteOut("[Debug] " + (string.IsNullOrWhiteSpace(flag) ? "" : $"[{flag}] "));
			WriteOutLine(message, Color);
		}
		return $"{DateTime.Now.ToString("HH:mm:ss.fff")}[{Name}] [Debug] " + message;
	}

	public static void Error(Exception e) => Error(null, e);

	public static void Error(string message, Exception Error = null, string Name = "HexedTools") {
        WriteOut($"[{DateTime.Now:HH:mm}] ", ConsoleColor.DarkYellow);

        WriteOut($"[{Name}] ", ConsoleColor.Red);
		WriteOut(string.IsNullOrEmpty(message) ? "Unknown Error: " : message + "\n", ConsoleColor.White);
		if (Error != null) WriteOutLine($"{Error}", ConsoleColor.Red);
	}

    public static string ConsoleColorToHex(ConsoleColor color) => color switch {
        ConsoleColor.Black => "#000000",
        ConsoleColor.DarkBlue => "#000080",
        ConsoleColor.DarkGreen => "#008000",
        ConsoleColor.DarkCyan => "#008080",
        ConsoleColor.DarkRed => "#5F0000",//"#800000",
        ConsoleColor.DarkMagenta => "#870087", //"#800080",
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

    public static string ConvertHexToAsci(string hexColor) {
        if (hexColor.StartsWith("#") && hexColor.Length == 7) {
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

    public static ConsoleColor GetRandomConsoleColor() {
        var ca = new Random().Next(1, 16);
        var color = (ConsoleColor)ca;
        if (color == lastcolor)
            color = (ConsoleColor)Enum.GetValues(typeof(ConsoleColor)).GetValue(ca - 1);
        if (color == ConsoleColor.Black || color == ConsoleColor.Red)
            return ConsoleColor.DarkMagenta;
        lastcolor = color;
        return color;
    }
}


public class MethodCallTextWriter : TextWriter { // Basic use
    private readonly Action<string> _outputMethod;

    public MethodCallTextWriter(Action<string> outputMethod) {
        _outputMethod = outputMethod;
    }

    public override Encoding Encoding => Encoding.Default;

    public override void Write(char value) {
        _outputMethod(value.ToString());
    }

    public override void Write(string value) {
        _outputMethod(value);
    }

    public override void WriteLine(string value) {
        _outputMethod(value + Environment.NewLine);
    }
}