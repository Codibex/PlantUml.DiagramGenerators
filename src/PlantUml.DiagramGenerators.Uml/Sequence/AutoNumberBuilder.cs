namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class AutoNumberBuilder : StatementBuilderBase<AutoNumberOptions>
{
    protected override string GetStatement()
    {
        string autoNumberBreak = GetAutoNumberBreak();
        string number = GetNumberStatement();
        string increment = GetIncrementStatement();
        string style = GetStyleStatement();
        return $"autonumber{number}{autoNumberBreak}{increment}{style}";
    }

    private string GetAutoNumberBreak() =>
        Options.AutoNumberBreak switch
        {
            AutoNumberBreak.Stop => " stop",
            AutoNumberBreak.Resume => " resume",
            null => string.Empty,
            _ => throw new ArgumentOutOfRangeException()
        };

    private string GetNumberStatement() =>
        string.IsNullOrWhiteSpace(Options.StartNumber)
            ? string.Empty
            : $" {Options.StartNumber}";

    private string GetIncrementStatement() =>
        string.IsNullOrWhiteSpace(Options.Increment)
            ? string.Empty
            : Options.Increment.Any(char.IsLetter)
                ? $" inc {Options.Increment}"
                : $" {Options.Increment}";

    private string GetStyleStatement() =>
        string.IsNullOrWhiteSpace(Options.Style)
            ? string.Empty
            : $" \"{Options.Style}\"";
}