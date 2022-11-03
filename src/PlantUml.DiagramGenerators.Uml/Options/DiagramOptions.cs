namespace PlantUml.DiagramGenerators.Uml.Options;

/// <summary>
/// Base diagram options
/// </summary>
public abstract class DiagramOptions
{
    private List<string> Options { get; } = new();

    public IEnumerable<string> AdditionalOptions => Options;

    public DiagramOptions AddOptions(params string[] options)
    {
        Options.AddRange(options);
        return this;
    }

    /// <summary>
    /// Returns all DiagramOption properties from the derived DiagramOptions classes
    /// </summary>
    /// <returns>List of DiagramOption</returns>
    public IEnumerable<DiagramOption> GetAllDefinedOptions()
    {
        return GetType()
            .GetProperties()
            .Where(p => p.PropertyType == typeof(DiagramOption))
            .Select(p => p.GetValue(this))
            .Cast<DiagramOption>();
    }
}