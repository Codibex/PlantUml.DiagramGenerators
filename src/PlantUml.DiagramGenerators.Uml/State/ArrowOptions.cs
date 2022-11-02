namespace PlantUml.DiagramGenerators.Uml.State;

public class ArrowOptions
{
    public byte Length { get; set; } = 2;
    public ArrowDirection? Direction { get; set; }
    public string? Color { get; set; }
    public string? Style { get; set; }
}