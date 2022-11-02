namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class NoteOptions
{
    private readonly List<string> _participants = new();
    public string Note { get; }
    public NotePosition Position { get; }
    public string? Color { get; private set; }

    public NoteOptions(string note, NotePosition position)
    {
        Note = note;
        Position = position;
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

    public string GetParticipants => string.Join(", ", _participants);
}