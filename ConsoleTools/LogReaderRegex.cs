using System.Text.RegularExpressions;

internal static partial class LogReaderRegex
{
    [GeneratedRegex(@"<color=(.*?)>(.*?)<\/color>")]
    internal static partial Regex ColorTagRegex();
}
