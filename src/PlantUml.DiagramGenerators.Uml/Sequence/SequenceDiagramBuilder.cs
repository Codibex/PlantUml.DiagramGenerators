namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramBuilder : UmlDiagramBuilderBase<SequenceUmlBuilder, SequenceDiagramOptions>
{
    public SequenceDiagramBuilder() : base(new SequenceUmlBuilder(0), SequenceDiagramOptions.Default)
    {
        
    }

    public SequenceDiagramBuilder AddSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription = null, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddSequence(sourceParticipant, targetParticipant, sequenceDescription, arrowConfig);
        return this;
    }

    public SequenceDiagramBuilder AddSequence(SequenceOptions sequenceOptions, Action<ArrowOptions>? arrowConfig = null)
    {
        UmlBuilder.AddSequence(sequenceOptions, arrowConfig);
        return this;
    }

    public SequenceDiagramBuilder AddParticipant(string participantName, string alias)
    {
        UmlBuilder.AddParticipant(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddParticipant(ParticipantOptions participantOptions)
    {
        UmlBuilder.AddParticipant(participantOptions);
        return this;
    }

    public SequenceDiagramBuilder AddActor(string participantName, string alias)
    {
        UmlBuilder.AddActor(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddBoundary(string participantName, string alias)
    {
        UmlBuilder.AddBoundary(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddControl(string participantName, string alias)
    {
        UmlBuilder.AddControl(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddEntity(string participantName, string alias)
    {
        UmlBuilder.AddEntity(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddDatabase(string participantName, string alias)
    {
        UmlBuilder.AddDatabase(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddCollections(string participantName, string alias)
    {
        UmlBuilder.AddCollections(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddQueue(string participantName, string alias)
    {
        UmlBuilder.AddQueue(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddAutoNumber(Action<AutoNumberOptions>? config = null)
    {
        UmlBuilder.AddAutoNumber(config);
        return this;
    }

    public SequenceDiagramBuilder AddNote(string note, NotePosition position)
    {
        UmlBuilder.AddNote(note, position);
        return this;
    }

    public SequenceDiagramBuilder AddPageHeader(string header)
    {
        UmlBuilder.AddPageHeader(header);
        return this;
    }

    public SequenceDiagramBuilder AddPageFooter(string footer, bool includePageNumber)
    {
        UmlBuilder.AddPageFooter(footer, includePageNumber);
        return this;
    }

    public SequenceDiagramBuilder AddPageTitle(string title)
    {
        UmlBuilder.AddPageTitle(title);
        return this;
    }
}