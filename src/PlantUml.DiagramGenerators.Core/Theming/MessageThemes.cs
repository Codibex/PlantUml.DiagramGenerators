namespace PlantUml.DiagramGenerators.Core.Theming;

/// <summary>
/// Message themes
/// </summary>
/// <remarks>
/// For details look at https://plantuml.com/en/theme
/// </remarks>
public static class MessageThemes
{
    public static string Success(string message) => $"$success(\"{message}\"";
    public static string Warning(string message) => $"warning(\"{message}\"";
    public static string Failure(string message) => $"failure(\"{message}\"";
}