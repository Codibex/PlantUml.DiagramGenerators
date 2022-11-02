namespace PlantUml.DiagramGenerators.Uml.Sequence;

public record AutoNumberOptions
{
    public string? StartNumber { get; set; }
    public string? Increment { get; set; }
    public string? Style { get; set; }
    public AutoNumberBreak? AutoNumberBreak { get; set; }
};