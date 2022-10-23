namespace PlantUml.DiagramGenerators.Core;

public static class Statements
{
    public static string DefineVariable(string variable, int value) => $"!{variable} = {value}";
    public static string DefineVariable(string variable, string value) => $"!{variable} = \"{value}\"";
}