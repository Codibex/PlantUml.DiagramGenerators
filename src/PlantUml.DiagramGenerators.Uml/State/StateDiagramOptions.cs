using PlantUml.DiagramGenerators.Uml.Options;

namespace PlantUml.DiagramGenerators.Uml.State;

/// <summary>
/// Diagram options
/// </summary>
public class StateDiagramOptions : DiagramOptions
{
    public static StateDiagramOptions Default => new()
    {
        HideEmptyDescriptionTag = { IsActive = true }
    };

    public DiagramOption HideEmptyDescriptionTag { get; } = DiagramOption.HideEmptyDescriptionTag;
}