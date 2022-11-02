namespace PlantUml.DiagramGenerators.Uml.Options;
public partial class SkinParameter
{
    public static SkinParameter SequenceMessageAlignment(SequenceMessageAlignment messageAlignment)
    {
        string alignment = messageAlignment switch
        {
            Uml.SequenceMessageAlignment.Left => "left",
            Uml.SequenceMessageAlignment.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(messageAlignment), messageAlignment, null)
        };

        return new SkinParameter($"sequenceMessageAlign {alignment}");
    }

    public static SkinParameter ResponseMessageBelowArrow(bool value)
    {
        return new SkinParameter($"responseMessageBelowArrow {value.ToString().ToLower()}");
    }

    public static SkinParameter StrictUml()
    {
        return new SkinParameter("style strictuml");
    }

    public static SkinParameter HideUnlinked()
    {
        return new SkinParameter("style strictuml");
    }

    public static SkinParameter ParticipantPadding(int padding)
    {
        return new SkinParameter($"ParticipantPadding {padding}");
    }

    public static SkinParameter BoxPadding(int padding)
    {
        return new SkinParameter($"BoxPadding {padding}");
    }
}
