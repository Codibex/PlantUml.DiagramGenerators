namespace PlantUml.DiagramGenerators.Uml.Sequence;

public record AutoNumber(int? StartNumber, int? Increment)
{
    public string GetStatement()
    {
        var number = GetNumberStatement();
        var increment = GetIncrementStatement();

        return $"autonumber{number}{increment}";
    }

    private string GetNumberStatement() =>
        StartNumber is null
            ? string.Empty
            : $" {StartNumber}";

    private string GetIncrementStatement() =>
        Increment is null
            ? string.Empty
            : $" {Increment}";
};