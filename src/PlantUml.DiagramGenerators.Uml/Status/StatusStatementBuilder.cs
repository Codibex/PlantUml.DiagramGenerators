namespace PlantUml.DiagramGenerators.Uml.Status;

internal class StatusStatementBuilder : StatementBuilderBase<StatusOptions>
{
    public StatusStatementBuilder(StatusOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        string statusName = Options.GetStatusName();
        string statusString = Options.AddStateStatement
            ? $"state {statusName}"
            : $"{statusName}";
        statusString = AppendAlias(statusString);
        statusString = AppendColor(statusString);
        statusString = AppendType(statusString);
        statusString = AppendDescription(statusString);

        return statusString;
    }

    protected override void CheckOptions()
    {
        if (Options.Type != StatusType.Fork && Options.Type != StatusType.Join)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Options.Description) == false)
        {
            throw new Exception("Fork and join cannot have a description");
        }
    }

    private string AppendColor(string statusString) =>
        string.IsNullOrWhiteSpace(Options.Color)
            ? statusString
            : $"{statusString} {Options.Color}";

    private string AppendAlias(string statusString)
    {
        if (string.IsNullOrWhiteSpace(Options.Alias))
        {
            return statusString;
        }

        string? alias = Options.Alias!.Split(' ').Length == 1
            ? Options.Alias
            : $"\"{Options.Alias}\"";

        return $"{statusString} as {alias}";
    }

    private string AppendDescription(string statusString)
        => string.IsNullOrWhiteSpace(Options.Description)
            ? statusString
            : $"{statusString} : {Options.Description}";

    private string AppendType(string statusString)
    {
        return Options.Type switch
        {
            StatusType.Unspecified => statusString,
            StatusType.Start => $"{statusString} <<start>>",
            StatusType.Choice => $"{statusString} <<choice>>",
            StatusType.Fork => $"{statusString} <<fork>>",
            StatusType.Join => $"{statusString} <<join>>",
            StatusType.End => $"{statusString} <<end>>",
            StatusType.EntryPoint => $"{statusString} <<entryPoint>>",
            StatusType.ExitPoint => $"{statusString} <<exitPoint>>",
            StatusType.InputPin => $"{statusString} <<inputPin>>",
            StatusType.OutputPin => $"{statusString} <<outputPin>>",
            StatusType.ExpansionInput => $"{statusString} <<expansionInput>>",
            StatusType.ExpansionOutput => $"{statusString} <<expansionOutput>>",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}