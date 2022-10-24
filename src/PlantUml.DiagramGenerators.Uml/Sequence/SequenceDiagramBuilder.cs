﻿using System.Text;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceDiagramBuilder
{
    private readonly SequenceBuilder _builder = new(0);

    public SequenceDiagramBuilder AddSequence(string sourceParticipant, string targetParticipant, string? sequenceDescription = null, ArrowOptions? arrowOptions = null)
    {
        _builder.AddSequence(sourceParticipant, targetParticipant, sequenceDescription, arrowOptions);
        return this;
    }

    public SequenceDiagramBuilder AddSequence(Sequence sequence, ArrowOptions? arrowOptions = null)
    {
        _builder.AddSequence(sequence, arrowOptions);
        return this;
    }

    public SequenceDiagramBuilder AddParticipant(string participantName, string alias)
    {
        _builder.AddParticipant(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddParticipant(Participant participant)
    {
        _builder.AddParticipant(participant);
        return this;
    }

    public SequenceDiagramBuilder AddActor(string participantName, string alias)
    {
        _builder.AddActor(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddBoundary(string participantName, string alias)
    {
        _builder.AddBoundary(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddControl(string participantName, string alias)
    {
        _builder.AddControl(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddEntity(string participantName, string alias)
    {
        _builder.AddEntity(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddDatabase(string participantName, string alias)
    {
        _builder.AddDatabase(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddCollections(string participantName, string alias)
    {
        _builder.AddCollections(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddQueue(string participantName, string alias)
    {
        _builder.AddQueue(participantName, alias);
        return this;
    }

    public SequenceDiagramBuilder AddAutoNumber(string? startNumber = null, string? increment = null,
        string? style = null, AutoNumberBreak? @break = null)
    {
        _builder.AddAutoNumber(startNumber, increment, style, @break);
        return this;
    }

    public SequenceDiagramBuilder AddNote(string note, NotePosition position)
    {
        _builder.AddNote(note, position);
        return this;
    }

    public string Build(Action<SequenceDiagramOptions>? options = null)
    {
        var currentOptions = SequenceDiagramOptions.Default;
        options?.Invoke(currentOptions);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(UmlConstants.START_TAG);
        
        foreach (string additionalOption in currentOptions.AdditionalOptions)
        {
            stringBuilder.AppendLine(additionalOption);
        }

        stringBuilder.AppendLine(_builder.Build());
        stringBuilder.AppendLine(UmlConstants.END_TAG);

        return stringBuilder.ToString().TrimEnd();
    }
}