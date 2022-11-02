namespace PlantUml.DiagramGenerators.Uml.State;

public class TransitionNoteStatementBuilder : StatementBuilderBase<TransitionNoteOptions>
{

    public TransitionNoteStatementBuilder(TransitionNoteOptions noteOptions) : base(noteOptions)
    {
    }

    protected override string GetStatement()
        => $"note on link{Environment.NewLine}{Options.Note}{Environment.NewLine}end note";
}