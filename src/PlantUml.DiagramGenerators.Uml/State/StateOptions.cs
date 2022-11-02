namespace PlantUml.DiagramGenerators.Uml.State;

/// <summary>
/// Options to configure complex state
/// </summary>
public class StateOptions
{
    /// <summary>
    /// State name
    /// </summary>
    public string StateName { get; }

    /// <summary>
    /// State alias
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// State description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Defines the State type
    /// </summary>
    public StateType Type { get; init; }

    /// <summary>
    /// Defines if the state name should be prefixed with 'state'
    /// </summary>
    internal bool AddStateStatement { get; set; } = false;

    public StateNoteOptions? NoteOptions { get; init; }
    public string? Color { get; init; }

    public StateOptions(string stateName)
    {
        StateName = stateName;
    }

    public string GetStateName()
    {
        if (string.IsNullOrWhiteSpace(Alias))
        {
            return StateName;
        }

        return StateName.Split(' ').Length == 1 && Type == StateType.Unspecified
            ? StateName
            : $"\"{StateName}\"";
    }
}