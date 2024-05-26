using HexedTools.ModulesLibs;

namespace Il2CppInterop.Internal.XrefScans;

public static class GeneratedDatabasesUtil
{
    public static string GetDatabasePath(string databaseName) => Path.Combine(PathDataInfo.RootFilePath, databaseName);
}
