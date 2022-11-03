namespace PlantUml.DiagramGenerators.Uml.Options;
public class SequenceSkinParameter : SkinParameter
{
    private SequenceSkinParameter(string parameter) : base(parameter)
    {
    }

    public static SkinParameter SequenceMessageAlignment(SequenceMessageAlignment messageAlignment)
    {
        string alignment = messageAlignment switch
        {
            Uml.SequenceMessageAlignment.Left => "left",
            Uml.SequenceMessageAlignment.Right => "right",
            _ => throw new ArgumentOutOfRangeException(nameof(messageAlignment), messageAlignment, null)
        };

        return new SequenceSkinParameter($"sequenceMessageAlign {alignment}");
    }

    public static SkinParameter ResponseMessageBelowArrow(bool value)
    {
        return new SequenceSkinParameter($"responseMessageBelowArrow {value.ToString().ToLower()}");
    }

    public static SkinParameter StrictUml()
    {
        return new SequenceSkinParameter("style strictuml");
    }

    public static SkinParameter HideUnlinked()
    {
        return new SequenceSkinParameter("style strictuml");
    }

    public static SkinParameter ParticipantPadding(byte padding)
    {
        return new SequenceSkinParameter($"ParticipantPadding {padding}");
    }

    public static SkinParameter BoxPadding(byte padding)
    {
        return new SequenceSkinParameter($"BoxPadding {padding}");
    }

    public static SkinParameter ActorBorderColor(string color)
    {
        return new SequenceSkinParameter($"ActorBorderColor {color}");
    }

    public static SkinParameter LifeLineBorderColor(string color)
    {
        return new SequenceSkinParameter($"sequenceLifeLineBorderColor {color}");
    }

    public static SkinParameter LifeLineBackgroundColor(string color)
    {
        return new SequenceSkinParameter($"sequenceLifeLineBackgroundColor {color}");
    }

    public static SkinParameter ParticipantBorderColor(string color)
    {
        return new SequenceSkinParameter($"sequenceParticipantBorderColor {color}");
    }

    public static SkinParameter ParticipantBackgroundColor(string color)
    {
        return new SequenceSkinParameter($"sequenceParticipantBackgroundColor {color}");
    }

    public static SkinParameter ParticipantFontName(string color)
    {
        return new SequenceSkinParameter($"sequenceParticipantFontName {color}");
    }

    public static SkinParameter ParticipantFontSize(byte fontSize)
    {
        return new SequenceSkinParameter($"sequenceParticipantFontSize {fontSize}");
    }

    public static SkinParameter ParticipantFontColor(string color)
    {
        return new SequenceSkinParameter($"sequenceParticipantFontColor {color}");
    }

    public static SkinParameter ActorBackgroundColor(string color)
    {
        return new SequenceSkinParameter($"sequenceActorBackgroundColor {color}");
    }

    public static SkinParameter ActorFontColor(string color)
    {
        return new SequenceSkinParameter($"sequenceActorFontColor {color}");
    }

    public static SkinParameter ActorFontSize(byte fontSize)
    {
        return new SequenceSkinParameter($"sequenceActorFontSize {fontSize}");
    }

    public static SkinParameter ActorFontName(string color)
    {
        return new SequenceSkinParameter($"sequenceActorFontName {color}");
    }
}
