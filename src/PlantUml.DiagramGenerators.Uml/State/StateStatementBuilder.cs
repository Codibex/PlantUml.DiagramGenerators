namespace PlantUml.DiagramGenerators.Uml.State;

internal class StateStatementBuilder : StatementBuilderBase<StateOptions>
{
    public StateStatementBuilder(StateOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        string stateName = Options.GetStateName();
        string stateString = Options.AddStateStatement
            ? $"state {stateName}"
            : $"{stateName}";
        stateString = AppendAlias(stateString);
        stateString = AppendColor(stateString);
        stateString = AppendType(stateString);
        stateString = AppendDescription(stateString);

        return stateString;
    }

    protected override void CheckOptions()
    {
        if (Options.Type != StateType.Fork && Options.Type != StateType.Join)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(Options.Description) == false)
        {
            throw new Exception("Fork and join cannot have a description");
        }
    }

    private string AppendColor(string stateString) =>
        string.IsNullOrWhiteSpace(Options.Color)
            ? stateString
            : $"{stateString} {Options.Color}";

    private string AppendAlias(string stateString)
    {
        if (string.IsNullOrWhiteSpace(Options.Alias))
        {
            return stateString;
        }

        string? alias = Options.Alias!.Split(' ').Length == 1
            ? Options.Alias
            : $"\"{Options.Alias}\"";

        return $"{stateString} as {alias}";
    }

    private string AppendDescription(string stateString)
        => string.IsNullOrWhiteSpace(Options.Description)
            ? stateString
            : $"{stateString} : {Options.Description}";

    private string AppendType(string stateString)
    {
        return Options.Type switch
        {
            StateType.Unspecified => stateString,
            StateType.Start => $"{stateString} <<start>>",
            StateType.Choice => $"{stateString} <<choice>>",
            StateType.Fork => $"{stateString} <<fork>>",
            StateType.Join => $"{stateString} <<join>>",
            StateType.End => $"{stateString} <<end>>",
            StateType.EntryPoint => $"{stateString} <<entryPoint>>",
            StateType.ExitPoint => $"{stateString} <<exitPoint>>",
            StateType.InputPin => $"{stateString} <<inputPin>>",
            StateType.OutputPin => $"{stateString} <<outputPin>>",
            StateType.ExpansionInput => $"{stateString} <<expansionInput>>",
            StateType.ExpansionOutput => $"{stateString} <<expansionOutput>>",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}