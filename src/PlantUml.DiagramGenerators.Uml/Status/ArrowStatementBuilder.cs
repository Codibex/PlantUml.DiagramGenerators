namespace PlantUml.DiagramGenerators.Uml.Status;

internal class ArrowStatementBuilder : StatementBuilderBase<ArrowOptions>
{
    public ArrowStatementBuilder() : base(new ArrowOptions())
    {
    }

    protected override string GetStatement()
    {
        string arrowDirectionText = GetArrowDirectionStatement();
        string colorAndStyleText = GetColorAndStyleStatement();
        return $"-{arrowDirectionText}{colorAndStyleText}{string.Join("", Enumerable.Range(0, Options.Length - 1).Select(_ => "-"))}>";
    }

    private string GetArrowDirectionStatement()
        => Options.Direction switch
        {
            ArrowDirection.Down => "down",
            ArrowDirection.Right => "right",
            ArrowDirection.Left => "left",
            ArrowDirection.Up => "up",
            null => string.Empty,
            _ => throw new ArgumentOutOfRangeException(nameof(Options.Direction), Options.Direction, null)
        };

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
