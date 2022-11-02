using PlantUml.DiagramGenerators.Uml.State;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ParticipantOptions
{
    private List<NoteOptions> _noteOptions = new();
    internal string Name { get; }
    internal string? Alias { get; }
    internal ParticipantType Type { get; }
    internal string? Color { get; private set; }

    internal int? Order { get; private set; }

    internal string? Declaration { get; private set; }

    public IReadOnlyList<NoteOptions> NoteOptions => _noteOptions;

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

    public ParticipantOptions WithNote(string note, NotePosition notePosition, string? color = null)
    {
        var noteOption = new NoteOptions(note, notePosition)
            
            .WithParticipant(this);

        if (color is not null)
        {
            noteOption = noteOption.WithColor(color);
        }

        _noteOptions.Add(noteOption);
        return this;
    }

    public string GetName()
    {
        if (string.IsNullOrWhiteSpace(Alias))
        {
            return Name;
        }

        return Name.Split(' ').Length == 1
            ? Name
            : $"\"{Name}\"";
    }
}