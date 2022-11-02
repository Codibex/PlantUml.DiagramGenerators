namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class ParticipantStatementBuilder : StatementBuilderBase<ParticipantOptions>
{
    internal ParticipantStatementBuilder(ParticipantOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        string participantStatement = Options.Type switch
        {
            ParticipantType.Participant => "participant",
            ParticipantType.Actor => "actor",
            ParticipantType.Boundary => "boundary",
            ParticipantType.Control => "control",
            ParticipantType.Entity => "entity",
            ParticipantType.Database => "database",
            ParticipantType.Collections => "collections",
            ParticipantType.Queue => "queue",
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Options.Type, null)
        };

        participantStatement = $"{participantStatement} {GetName()}";
        participantStatement = AppendDeclaration(participantStatement);
        participantStatement = AppendAlias(participantStatement);
        participantStatement = AppendOrder(participantStatement);
        participantStatement = AppendColor(participantStatement);

        return participantStatement;
    }

    private string GetName()
    {
        if (string.IsNullOrWhiteSpace(Options.Alias))
        {
            return Options.Name;
        }

        return Options.Name.Split(' ').Length == 1
            ? Options.Name
            : $"\"{Options.Name}\"";
    }

    private string AppendDeclaration(string participantStatement)
    {
        if (string.IsNullOrWhiteSpace(Options.Declaration))
        {
            return participantStatement;
        }

        string lines = string.Join(Environment.NewLine, Options.Declaration.Split(Environment.NewLine).Select(l => $"\t{l}"));
        return $"{participantStatement} [{Environment.NewLine}{lines}{Environment.NewLine}]";
    }

    private string AppendAlias(string participantStatement) =>
        string.IsNullOrWhiteSpace(Options.Alias)
            ? participantStatement
            : $"{participantStatement} as {Options.Alias}";

    private string AppendOrder(string participantStatement) =>
        Options.Order is null
            ? participantStatement
            : $"{participantStatement} order {Options.Order}";

    private string AppendColor(string participantStatement) =>
        string.IsNullOrWhiteSpace(Options.Color)
            ? participantStatement
            : $"{participantStatement} {Options.Color}";
}