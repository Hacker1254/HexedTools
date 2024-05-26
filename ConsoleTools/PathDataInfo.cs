using System;

namespace HexedTools.ModulesLibs;

public class PathDataInfo {
    public static string RootFilePath { get; } = Directory.CreateDirectory("C:\\HexedTools").FullName; // Make sure is at top
    public static string LibsPath { get; } = Directory.CreateDirectory(RootFilePath + "\\UserLibs").FullName;
}