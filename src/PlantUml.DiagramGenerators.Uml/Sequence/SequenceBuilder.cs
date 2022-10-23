namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class SequenceBuilder : UmlBuilder
{
    public SequenceBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public SequenceBuilder AddSequence(string sourceParticipant, string targetParticipant)
    {

        return this;
    }
}
