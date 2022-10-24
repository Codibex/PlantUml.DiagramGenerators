namespace PlantUml.DiagramGenerators.Uml.Sequence;

public record AutoNumber(int? StartNumber, int? Increment, string? Style)
{
    public string GetStatement()
    {
        var number = GetNumberStatement();
        var increment = GetIncrementStatement();
        var style = GetStyleStatement();
        return $"autonumber{number}{increment}{style}";
    }

    private string GetNumberStatement() =>
        StartNumber is null
            ? string.Empty
            : $" {StartNumber}";

    private string GetIncrementStatement() =>
        Increment is null
            ? string.Empty
            : $" {Increment}";

    private string GetStyleStatement() =>
        Style is null
            ? string.Empty
            : $" \"{Style}\"";
};