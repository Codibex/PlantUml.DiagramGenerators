namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ParticipantBuilder
{
    private string Name { get; }
    private string? Alias { get; }
    private ParticipantType Type { get; }
    private string? Color { get; set; }

    private int? Order { get; set; }

    private string? Declaration { get; set; }

    private ParticipantBuilder(string name, string? alias, ParticipantType type)
    {
        Name = name;
        Alias = alias;
        Type = type;
    }

    public static ParticipantBuilder CreateParticipant(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Participant);

    public static ParticipantBuilder CreateActor(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Actor);

    public static ParticipantBuilder CreateBoundary(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Boundary);

    public static ParticipantBuilder CreateControl(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Control);

    public static ParticipantBuilder CreateEntity(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Entity);

    public static ParticipantBuilder CreateDatabase(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Database);

    public static ParticipantBuilder CreateCollections(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Collections);

    public static ParticipantBuilder CreateQueue(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Queue);

    public ParticipantBuilder WithColor(string color)
    {
        Color = color;
        return this;
    }

    public ParticipantBuilder WithOrder(int order)
    {
        Order = order;
        return this;
    }

    public ParticipantBuilder WithMultilineDeclaration(string declaration)
    {
        Declaration = declaration;
        return this;
    }

    public string Build()
    {
        string participantStatement = Type switch
        {
            ParticipantType.Participant => "participant",
            ParticipantType.Actor => "actor",
            ParticipantType.Boundary => "boundary",
            ParticipantType.Control => "control",
            ParticipantType.Entity => "entity",
            ParticipantType.Database => "database",
            ParticipantType.Collections => "collections",
            ParticipantType.Queue => "queue",
            _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, null)
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
        if (string.IsNullOrWhiteSpace(Alias))
        {
            return Name;
        }

        return Name.Split(' ').Length == 1
            ? Name
            : $"\"{Name}\"";
    }

    private string AppendDeclaration(string participantStatement)
    {
        if (string.IsNullOrWhiteSpace(Declaration))
        {
            return participantStatement;
        }
        
        string lines = string.Join(Environment.NewLine, Declaration.Split(Environment.NewLine).Select(l => $"\t{l}"));
        return $"{participantStatement} [{Environment.NewLine}{lines}{Environment.NewLine}]";
    }

    private string AppendAlias(string participantStatement) =>
        string.IsNullOrWhiteSpace(Alias)
            ? participantStatement
            : $"{participantStatement} as {Alias}";

    private string AppendOrder(string participantStatement) =>
        Order is null
            ? participantStatement
            : $"{participantStatement} order {Order}";

    private string AppendColor(string participantStatement) =>
        string.IsNullOrWhiteSpace(Color)
            ? participantStatement
            : $"{participantStatement} {Color}";
}