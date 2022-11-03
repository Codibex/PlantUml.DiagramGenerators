namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ArrowOptions
{
    public ArrowLineStyle LineStyle { get; set; } = ArrowLineStyle.Normal;
    public string? Color { get; set; }
    public string? Style { get; set; }
    internal ArrowDirection Direction { get; set; }
}