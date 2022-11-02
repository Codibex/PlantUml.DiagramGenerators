namespace PlantUml.DiagramGenerators.Uml.State;

public class StateNoteStatementBuilder : StatementBuilderBase<StateOptions>
{
    public StateNoteStatementBuilder(StateOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        string[] lines = Options.NoteOptions!.Note.Split(Environment.NewLine);
        var note = $"note {GetPositionStatement()} of {Options.GetStateName()}";
        if (lines.Length == 1)
        {
            return $"{note} : {Options.NoteOptions!.Note}";
        }

        return $"{note}{Environment.NewLine}{Options.NoteOptions!.Note}{Environment.NewLine}end note";
    }

    private string GetPositionStatement()
        => Options.NoteOptions!.Position switch
        {
            NotePosition.Left => "left",
            NotePosition.Top => "top",
            NotePosition.Right => "right",
            NotePosition.Bottom => "bottom",
            _ => throw new ArgumentOutOfRangeException(nameof(Options.NoteOptions.Position), Options.NoteOptions!.Position, null)
        };
}