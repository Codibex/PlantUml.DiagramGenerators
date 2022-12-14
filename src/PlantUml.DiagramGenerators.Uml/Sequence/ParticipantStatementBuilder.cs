using PlantUml.DiagramGenerators.Uml.Utilities;

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

        participantStatement = $"{participantStatement} {Options.GetName()}";
        participantStatement = AppendDeclaration(participantStatement);
        participantStatement = AppendAlias(participantStatement);
        participantStatement = AppendOrder(participantStatement);
        participantStatement = AppendColor(participantStatement);

        return participantStatement;
    }

    

    private string AppendDeclaration(string participantStatement)
    {
        if (string.IsNullOrWhiteSpace(Options.Declaration))
        {
            return participantStatement;
        }

        string lines = Options.Declaration.AddTabsPerLine(1);
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