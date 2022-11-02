using System.Text;

namespace PlantUml.DiagramGenerators.Uml;

public abstract class UmlDiagramBuilderBase<TUmlBuilder, TOptions>
    where TUmlBuilder : UmlBuilder
    where TOptions : DiagramOptions
{
    protected TUmlBuilder UmlBuilder { get; }
    protected TOptions Options { get; }

    protected UmlDiagramBuilderBase(TUmlBuilder umlBuilder, TOptions options)
    {
        UmlBuilder = umlBuilder;
        Options = options;
    }

    public string Build(Action<TOptions>? config = null)
    {
        config?.Invoke(Options);

        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(UmlConstants.START_TAG);
        AddDiagramSpecificOptionsStatements(stringBuilder);
        foreach (string additionalOption in Options.AdditionalOptions)
        {
            stringBuilder.AppendLine(additionalOption);
        }

        stringBuilder.AppendLine(UmlBuilder.Build());
        stringBuilder.AppendLine(UmlConstants.END_TAG);

        return stringBuilder.ToString().TrimEnd();
    }

    protected virtual void AddDiagramSpecificOptionsStatements(StringBuilder stringBuilder)
    {
    }
}