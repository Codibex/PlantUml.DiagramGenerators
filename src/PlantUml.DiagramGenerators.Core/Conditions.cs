namespace PlantUml.DiagramGenerators.Core;

public static class Conditions
{
    public static string If(string expression) => $"!if {expression}";
    public static string ElseIf(string expression) => $"!elseif {expression}";
    public static string Else(string expression) => $"!else {expression}";
    public static string EndIf() => "!endif";
}