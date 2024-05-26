using System;

namespace HexedTools.ModulesLibs;

public class PathDataInfo {
    public static IntPtr CreateHook { get; internal set; } // LAZY! Don't do this i need to do it correctly later
    public static IntPtr RemoveHook { get; internal set; } // LAZY! Don't do this i need to do it correctly later

    public static string RootFilePath { get; } = Directory.CreateDirectory("C:\\HexedTools").FullName; // Make sure is at top
    public static string LibsPath { get; } = Directory.CreateDirectory(RootFilePath + "\\UserLibs").FullName;
}