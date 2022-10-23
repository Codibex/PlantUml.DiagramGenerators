namespace PlantUml.DiagramGenerators.Uml.Status;

internal class StatusBuilder
{
    private readonly StatusOptions _options;

    public StatusBuilder(StatusOptions options)
    {
        _options = options;
    }

    public string Build()
    {
        CheckOptions();

        string statusName = _options.GetStatusName();
        string statusString = _options.AddStateStatement
            ? $"state {statusName}"
            : $"{statusName}";
        statusString = AppendAlias(statusString);
        statusString = AppendColor(statusString);
        statusString = AppendType(statusString);
        statusString = AppendDescription(statusString);

        return statusString;
    }

    private void CheckOptions()
    {
        if (_options.Type != StatusType.Fork && _options.Type != StatusType.Join)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_options.Description) == false)
        {
            throw new Exception("Fork and join cannot have a description");
        }
    }

    private string AppendColor(string statusString) =>
        string.IsNullOrWhiteSpace(_options.Color)
            ? statusString
            : $"{statusString} {_options.Color}";

    private string AppendAlias(string statusString)
    {
        if (string.IsNullOrWhiteSpace(_options.Alias))
        {
            return statusString;
        }

        string? alias = _options.Alias!.Split(' ').Length == 1
            ? _options.Alias
            : $"\"{_options.Alias}\"";

        return $"{statusString} as {alias}";
    }

    private string AppendDescription(string statusString)
        => string.IsNullOrWhiteSpace(_options.Description)
            ? statusString
            : $"{statusString} : {_options.Description}";

    private string AppendType(string statusString)
    {
        return _options.Type switch
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