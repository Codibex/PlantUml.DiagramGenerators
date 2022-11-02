namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class ArrowStatementBuilder : StatementBuilderBase<ArrowOptions>
{
    internal ArrowStatementBuilder() : base(new ArrowOptions())
    {
    }

    protected override string GetStatement()
    {
        string colorAndStyleText = GetColorAndStyleStatement();

        string arrow = Options.LineStyle switch
        {
            ArrowLineStyle.Normal => Options.Direction == ArrowDirection.SourceToTarget ? "->" : "<-",
            ArrowLineStyle.Dotted => Options.Direction == ArrowDirection.SourceToTarget ? "-->" : "<--",
            _ => throw new ArgumentOutOfRangeException()
        };

        if (string.IsNullOrWhiteSpace(colorAndStyleText))
        {
            return arrow;
        }

        return arrow.Insert(1, colorAndStyleText);
    }

    private string GetColorAndStyleStatement()
    {
        bool colorSet = string.IsNullOrWhiteSpace(Options.Color) == false;
        bool styleSet = string.IsNullOrWhiteSpace(Options.Style) == false;

        if (colorSet == false && styleSet == false)
        {
            return string.Empty;
        }

        var statement = "[";
        if (colorSet)
        {
            statement += Options.Color;
        }

        if (styleSet == false)
        {
            return $"{statement}]";
        }

        if (colorSet)
        {
            statement += $",{Options.Style}";
        }
        else
        {
            statement += $"{Options.Style}";
        }

        return $"{statement}]";
    }
}
