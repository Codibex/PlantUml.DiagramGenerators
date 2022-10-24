namespace PlantUml.DiagramGenerators.Uml;
public class SkinParameter
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

    public static SkinParameter SequenceMessageAlignment(SequenceMessageAlignment messageAlignment)
    {
        string alignment = messageAlignment switch
        {
            Uml.SequenceMessageAlignment.Left => "left",
            Uml.SequenceMessageAlignment.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(messageAlignment), messageAlignment, null)
        };
        
        return new SkinParameter($"sequenceMessageAlign {alignment}");
    }

    public static SkinParameter ResponseMessageBelowArrow(bool value)
    {
        return new SkinParameter($"responseMessageBelowArrow {value.ToString().ToLower()}");
    }

    public static implicit operator string(SkinParameter parameter) => parameter.GetStatement();

    public string GetStatement()
    {
        return $"skinparam {Parameter}";
    }
}