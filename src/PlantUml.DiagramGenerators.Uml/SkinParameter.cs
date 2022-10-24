namespace PlantUml.DiagramGenerators.Uml;

public partial class SkinParameter
{
    private string Parameter { get; }

    private SkinParameter(string parameter)
    {
        Parameter = parameter;
    }

    public static SkinParameter BackgroundColor(string color)
        => new($"backgroundColor {color}");

    public static SkinParameter ClassFontColor(string color) 
        => new($"classFontColor {color}");

    public static implicit operator string(SkinParameter parameter) => parameter.GetStatement();

    public string GetStatement()
    {
        return $"skinparam {Parameter}";
    }
}