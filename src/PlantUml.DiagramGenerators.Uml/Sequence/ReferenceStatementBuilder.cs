using PlantUml.DiagramGenerators.Uml.Utilities;

namespace PlantUml.DiagramGenerators.Uml.Sequence;

public class ReferenceStatementBuilder : UmlBuilder
{
    public ReferenceStatementBuilder(int nestingDepth) : base(nestingDepth)
    {
    }

    public ReferenceStatementBuilder AddReference(string referenceDescription, IEnumerable<ParticipantOptions> participants)
    {
        string participantsStatement = string.Join(", ", participants.Select(p => p.GetName()));
        AddEntry($"ref over {participantsStatement}");
        AddEntry($"{referenceDescription.AddTabsPerLine(1)}");
        AddEntry("end ref");
        return this;
    }
}