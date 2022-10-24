namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class Participant
{
    public string Name { get; }
    public string? Alias { get; }
    public ParticipantType Type { get; }
    public string? Color { get; private set; }

    public int? Order { get; private set; }

    public string? Declaration { get; set; }

    private Participant(string name, string? alias, ParticipantType type)
    {
        Name = name;
        Alias = alias;
        Type = type;
    }

    public static Participant CreateParticipant(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Participant);

    public static Participant CreateActor(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Actor);

    public static Participant CreateBoundary(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Boundary);

    public static Participant CreateControl(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Control);

    public static Participant CreateEntity(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Entity);

    public static Participant CreateDatabase(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Database);

    public static Participant CreateCollections(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Collections);

    public static Participant CreateQueue(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Queue);

    public Participant WithColor(string color)
    {
        Color = color;
        return this;
    }

    public Participant WithOrder(int order)
    {
        Order = order;
        return this;
    }

    public Participant WithMultilineDeclaration(string declaration)
    {
        Declaration = declaration;
        return this;
    }

    public string GetStatement()
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