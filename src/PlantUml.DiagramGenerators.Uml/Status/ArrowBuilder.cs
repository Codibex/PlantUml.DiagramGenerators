namespace PlantUml.DiagramGenerators.Uml.Status;

internal class ArrowBuilder
{
    private readonly ArrowOptions _options;

    public ArrowBuilder(ArrowOptions options)
    {
        _options = options;
    }

    public string Build()
    {
        string arrowDirectionText = GetArrowDirectionStatement();
        string colorAndStyleText = GetColorAndStyleStatement();
        return $"-{arrowDirectionText}{colorAndStyleText}{string.Join("", Enumerable.Range(0, _options.Length - 1).Select(_ => "-"))}>";
    }

    private string GetArrowDirectionStatement()
        => _options.Direction switch
        {
            ArrowDirection.Down => "down",
            ArrowDirection.Right => "right",
            ArrowDirection.Left => "left",
            ArrowDirection.Up => "up",
            null => string.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(_options.Direction), _options.Direction, null)
        };

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
