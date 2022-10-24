namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal class ArrowBuilder
{
    private readonly ArrowOptions _options;

    public ArrowBuilder(ArrowOptions options)
    {
        _options = options;
    }

    public string Build()
    {
        string colorAndStyleText = GetColorAndStyleStatement();

        string arrow = _options.LineStyle switch
        {
            ArrowLineStyle.Normal => _options.Direction == ArrowDirection.SourceToTarget ? "->" : "<-",
            ArrowLineStyle.Dotted => _options.Direction == ArrowDirection.SourceToTarget ? "-->" : "<--",
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
        bool colorSet = string.IsNullOrWhiteSpace(_options.Color) == false;
        bool styleSet = string.IsNullOrWhiteSpace(_options.Style) == false;

        if (colorSet == false && styleSet == false)
        {
            return string.Empty;
        }

        var statement = "[";
        if (colorSet)
        {
            statement += _options.Color;
        }

        if (styleSet == false)
        {
            return $"{statement}]";
        }

        if (colorSet)
        {
            statement += $",{_options.Style}";
        }
        else
        {
            statement += $"{_options.Style}";
        }

        return $"{statement}]";
    }
}
