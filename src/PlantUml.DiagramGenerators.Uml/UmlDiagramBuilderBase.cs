using System.Text;
using PlantUml.DiagramGenerators.Uml.Options;

namespace PlantUml.DiagramGenerators.Uml;

/// <summary>
/// Base class for a concrete builder to build an uml diagram like sequence or state diagram
/// </summary>
/// <typeparam name="TUmlBuilder">Concrete uml builder type</typeparam>
/// <typeparam name="TOptions">Concrete builder type options</typeparam>
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

    /// <summary>
    /// Build the uml diagram with the provided configuration
    /// </summary>
    /// <param name="config">Action to configure the options</param>
    /// <returns>The uml string in the plant uml format</returns>
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

    protected void AddDiagramSpecificOptionsStatements(StringBuilder stringBuilder)
    {
        var options = Options.GetAllDefinedOptions();
        foreach (var diagramOption in options.Where(o => o.IsActive))
        {
            stringBuilder.AppendLine(diagramOption.GetStatement());
        }
    }
}