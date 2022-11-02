namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class NoteUmlBuilder : UmlBuilder
{
    public NoteUmlBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public NoteUmlBuilder AddNote(NoteOptions options)
    {
        string alignedStatement = GetAlignedStatement(options);
        string noteStartStatement = GetNoteStartStatement(options);
        string participantStatement = GetParticipantStatement(options);
        string positionStatement = GetPositionStatement(options);
        string colorStatement = GetColorStatement(options);
        string noteEndStatement = GetNoteEndStatement(options);

        AddEntry($"{alignedStatement}{noteStartStatement} {positionStatement}{participantStatement}{colorStatement}");
        AddEntry(options.Note);
        AddEntry(noteEndStatement);
        return this;
    }

    private static string GetAlignedStatement(NoteOptions options) =>
        options.Aligned
            ? "/ "
            : string.Empty;

    private static string GetNoteStartStatement(NoteOptions options) =>
        options.Shape switch
        {
            NoteShape.Undefined => "note",
            NoteShape.Rectangle => "rnote",
            NoteShape.Hexagonal => "hnote",
            _ => throw new ArgumentOutOfRangeException(nameof(options.Shape), options.Shape, null)
        };

    private static string GetNoteEndStatement(NoteOptions options)
    {
        return options.Shape switch
        {
            NoteShape.Undefined => "end note",
            NoteShape.Rectangle => "endrnote",
            NoteShape.Hexagonal => "endhnote",
            _ => throw new ArgumentOutOfRangeException(nameof(options.Shape), options.Shape, null)
        };
    }

    private static string GetParticipantStatement(NoteOptions options)
    {
        string participants = options.GetParticipants;
        if (string.IsNullOrWhiteSpace(participants))
        {
            return string.Empty;
        }

        string prefix = options.Position is NotePosition.Left or NotePosition.Right 
            ? " of" 
            : string.Empty;
        return  $"{prefix} {participants}";
    }

    private static string GetPositionStatement(NoteOptions options)
        => options.Position switch
        {
            NotePosition.Left => "left",
            NotePosition.Right => "right",
            NotePosition.Over => "over",
            NotePosition.Across => "across",
            _ => throw new ArgumentOutOfRangeException(nameof(options.Position), options.Position, null)
        };

    private static string GetColorStatement(NoteOptions options)
    {
        return string.IsNullOrWhiteSpace(options.Color) ? string.Empty : $" {options.Color}";
    }
}