namespace PlantUml.DiagramGenerators.Uml;

public class ArrowOptions
{
    public byte Length { get; init; } = 2;
    public ArrowDirection? Direction { get; init; }
    public string? Color { get; init; }
    public string? Style { get; init; }
}