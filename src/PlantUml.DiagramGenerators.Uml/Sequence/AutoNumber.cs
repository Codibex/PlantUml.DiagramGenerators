namespace PlantUml.DiagramGenerators.Uml.Sequence;

public record AutoNumber(string? StartNumber, string? Increment, string? Style, AutoNumberBreak? AutoNumberBreak)
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
        string.IsNullOrWhiteSpace(StartNumber)
            ? string.Empty
            : $" {StartNumber}";

    private string GetIncrementStatement() =>
        string.IsNullOrWhiteSpace(Increment)
            ? string.Empty
            : Increment.Any(char.IsLetter) 
                ? $" inc {Increment}"
                : $" {Increment}";

    private string GetStyleStatement() =>
        string.IsNullOrWhiteSpace(Style)
            ? string.Empty
            : $" \"{Style}\"";
};