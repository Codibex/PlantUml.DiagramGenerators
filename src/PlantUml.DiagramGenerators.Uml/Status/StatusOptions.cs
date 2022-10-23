namespace PlantUml.DiagramGenerators.Uml.Status;

/// <summary>
/// Options to configure complex status
/// </summary>
public class StatusOptions
{
    /// <summary>
    /// Status name
    /// </summary>
    public string StatusName { get; }

    /// <summary>
    /// Status alias
    /// </summary>
    public string? Alias { get; init; }

    /// <summary>
    /// Status description
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// Defines the status type
    /// </summary>
    public StatusType Type { get; init; }

    public bool AddStateStatement { get; internal set; } = true;

    public StatusNoteOptions? NoteOptions { get; init; }
    public string? Color { get; init; }

    public StatusOptions(string statusName)
    {
        StatusName = statusName;
    }

    public string GetStatusName()
    {
        if (string.IsNullOrWhiteSpace(Alias))
        {
            return StatusName;
        }

        return StatusName.Split(' ').Length == 1 && Type == StatusType.Unspecified
            ? StatusName
            : $"\"{StatusName}\"";
    }
}