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

    public SequenceDiagramBuilder AddSequence(ParticipantOptions sourceParticipant, ParticipantOptions targetParticipant, string? sequenceDescription = null, Action<ArrowOptions>? arrowConfig = null)
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

    public SequenceDiagramBuilder AddNote(NoteOptions noteOptions)
    {
        UmlBuilder.AddNote(noteOptions);
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

    public SequenceDiagramBuilder AddNewPage(string? title = null)
    {
        UmlBuilder.AddNewPage(title);
        return this;
    }

    public SequenceDiagramBuilder AddMessageGroup(Action<MessageGroupStatementBuilder> messageBuilderAction)
    {
        UmlBuilder.AddMessageGroup(messageBuilderAction);
        return this;
    }

    public SequenceDiagramBuilder AddGroup(string groupName, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        UmlBuilder.AddGroup(groupName, umlBuilderAction);
        return this;
    }

    public SequenceDiagramBuilder AddLoop(int times, Action<SequenceUmlBuilder> umlBuilderAction)
    {
        UmlBuilder.AddLoop(times, umlBuilderAction);
        return this;
    }

    public SequenceDiagramBuilder AddDivider(string dividerText)
    {
        UmlBuilder.AddDivider(dividerText);
        return this;
    }

    public SequenceDiagramBuilder AddReference(string referenceDescription, params ParticipantOptions[] participants)
    {
        UmlBuilder.AddReference(referenceDescription, participants);
        return this;
    }

    public SequenceDiagramBuilder AddDelay(string? delayMessage = null)
    {
        UmlBuilder.AddDelay(delayMessage);
        return this;
    }

    /// <summary>
    /// Adds a space
    /// </summary>
    /// <param name="space">Space in pixel</param>
    /// <returns>The current builder instance</returns>
    public SequenceDiagramBuilder AddSpace(int? space = null)
    {
        UmlBuilder.AddSpace(space);
        return this;
    }
}