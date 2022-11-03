namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class NoteOptions
{
    private readonly List<string> _participants = new();
    public string Note { get; }
    public NotePosition Position { get; }
    public NoteShape Shape { get; set; } = NoteShape.Undefined;
    public string? Color { get; private set; }

    public bool Aligned { get; private set; }

    public NoteOptions(string note, NotePosition position)
    {
        Note = note;
        Position = position;
    }

    public NoteOptions WithShape(NoteShape shape)
    {
        Shape = shape;
        return this;
    }

    public NoteOptions WithColor(string color)
    {
        Color = color;
        return this;
    }

    public NoteOptions WithParticipant(ParticipantOptions participant)
    {
        _participants.Add(participant.GetName());
        return this;
    }

    public NoteOptions WithAlignment()
    {
        Aligned = true;
        return this;
    }

    public string GetParticipants => string.Join(", ", _participants);
}