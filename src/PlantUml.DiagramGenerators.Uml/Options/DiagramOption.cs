namespace PlantUml.DiagramGenerators.Uml.Options;

public partial class DiagramOption
{
    private string Parameter { get; }

    public bool IsActive { get; set; }

    private DiagramOption(string parameter)
    {
        Parameter = parameter;
    }

    public static implicit operator string(DiagramOption parameter) => parameter.GetStatement();

    private string GetStatement()
    {
        return $"{Parameter}";
    }
}
