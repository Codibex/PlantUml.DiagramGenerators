namespace PlantUml.DiagramGenerators.Uml.Options;

public partial class DiagramOption
{
    private string Parameter { get; }

    public bool IsActive { get; set; }

    private DiagramOption(string parameter)
    {
        Parameter = parameter;
    }

    internal string GetStatement()
    {
        return $"{Parameter}";
    }
}
