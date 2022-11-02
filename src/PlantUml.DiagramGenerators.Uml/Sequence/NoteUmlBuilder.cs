namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class NoteUmlBuilder : UmlBuilder
{
    public NoteUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public NoteUmlBuilder AddNote(NoteOptions options)
    {
        string participantStatement = GetParticipantStatement(options);
        string positionStatement = GetPositionStatement(options);
        string colorStatement = GetColorStatement(options);

        AddEntry($"note {positionStatement}{participantStatement}{colorStatement}");
        AddEntry(options.Note);
        AddEntry("end note");
        return this;
    }

    private static string GetParticipantStatement(NoteOptions options)
    {
        string participants = options.GetParticipants;
        if (string.IsNullOrWhiteSpace(participants))
        {
            return string.Empty;
        }

        string prefix = options.Position == NotePosition.Over ? string.Empty : " of";
        return  $"{prefix} {participants}";
    }

    private static string GetPositionStatement(NoteOptions options)
        => options.Position switch
        {
            NotePosition.Left => "left",
            NotePosition.Right => "right",
            NotePosition.Over => "over",
            _ => throw new ArgumentOutOfRangeException(nameof(options.Position), options.Position, null)
        };

    private static string GetColorStatement(NoteOptions options)
    {
        return string.IsNullOrWhiteSpace(options.Color) ? string.Empty : $" {options.Color}";
    }
}