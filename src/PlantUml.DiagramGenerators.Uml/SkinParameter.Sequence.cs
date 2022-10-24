namespace PlantUml.DiagramGenerators.Uml;
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
}
