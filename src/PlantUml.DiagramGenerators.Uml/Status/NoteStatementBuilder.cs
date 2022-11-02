namespace PlantUml.DiagramGenerators.Uml.Status;

public class NoteStatementBuilder : StatementBuilderBase<NoteOptions>
{
    public NoteStatementBuilder(NoteOptions noteOptions) : base(noteOptions)
    {
    }

    protected override string GetStatement()
    {
        bool hasAlias = string.IsNullOrWhiteSpace(Options.Alias) == false;

        string note = hasAlias
            ? $"\"{Options.Note}\""
            : Options.Note;

        string alias = hasAlias
            ? $" as {Options.Alias}"
            : string.Empty;

        var noteStatement = $"note {note}{alias}";
        return noteStatement;
    }
}