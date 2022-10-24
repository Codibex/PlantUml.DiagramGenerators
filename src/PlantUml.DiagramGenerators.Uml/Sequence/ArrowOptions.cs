namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ArrowOptions
{
    public ArrowLineStyle LineStyle { get; init; } = ArrowLineStyle.Normal;
    public string? Color { get; init; }
    public string? Style { get; init; }
    internal ArrowDirection Direction { get; set; }
}