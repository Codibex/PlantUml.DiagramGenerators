namespace PlantUml.DiagramGenerators.Uml.State;

/// <summary>
/// State types
/// </summary>
public enum StateType
{
    Unspecified,
    Start,
    Choice,
    Fork,
    Join,
    End,
    EntryPoint,
    ExitPoint,
    InputPin,
    OutputPin,
    ExpansionInput,
    ExpansionOutput
}