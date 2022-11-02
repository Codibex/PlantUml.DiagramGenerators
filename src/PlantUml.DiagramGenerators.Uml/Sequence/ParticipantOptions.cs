namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ParticipantOptions
{
    internal string Name { get; }
    internal string? Alias { get; }
    internal ParticipantType Type { get; }
    internal string? Color { get; private set; }

    internal int? Order { get; private set; }

    internal string? Declaration { get; private set; }

    private ParticipantOptions(string name, string? alias, ParticipantType type)
    {
        Name = name;
        Alias = alias;
        Type = type;
    }

    public static ParticipantOptions CreateParticipant(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Participant);

    public static ParticipantOptions CreateActor(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Actor);

    public static ParticipantOptions CreateBoundary(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Boundary);

    public static ParticipantOptions CreateControl(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Control);

    public static ParticipantOptions CreateEntity(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Entity);

    public static ParticipantOptions CreateDatabase(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Database);

    public static ParticipantOptions CreateCollections(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Collections);

    public static ParticipantOptions CreateQueue(string name, string? alias = null) =>
        new(name, alias, ParticipantType.Queue);

    public ParticipantOptions WithColor(string color)
    {
        Color = color;
        return this;
    }

    public ParticipantOptions WithOrder(int order)
    {
        Order = order;
        return this;
    }

    public ParticipantOptions WithMultilineDeclaration(string declaration)
    {
        Declaration = declaration;
        return this;
    }
}