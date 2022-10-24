namespace PlantUml.DiagramGenerators.Uml.Sequence;

public record AutoNumber(int? StartNumber, int? Increment, string? Style, AutoNumberBreak? AutoNumberBreak)
{
    public string GetStatement()
    {
        string autoNumberBreak = GetAutoNumberBreak();
        string number = GetNumberStatement();
        string increment = GetIncrementStatement();
        string style = GetStyleStatement();
        return $"autonumber{number}{autoNumberBreak}{increment}{style}";
    }

    private string GetAutoNumberBreak() =>
        AutoNumberBreak switch
        {
            Uml.Sequence.AutoNumberBreak.Stop => " stop",
            Uml.Sequence.AutoNumberBreak.Resume => " resume",
            null => string.Empty,
            _ => throw new ArgumentOutOfRangeException()
        };

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