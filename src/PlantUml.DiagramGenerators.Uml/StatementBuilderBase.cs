namespace PlantUml.DiagramGenerators.Uml;

/// <summary>
/// Base class for statement builder
/// </summary>
/// <typeparam name="TOptions">Builder options</typeparam>
public abstract class StatementBuilderBase<TOptions>
{
    protected TOptions Options { get; }

    protected StatementBuilderBase(TOptions options)
    {
        Options = options;
    }

    internal string Build(Action<TOptions>? config = null)
    {
        config?.Invoke(Options);
        
        CheckOptions();
        
        return GetStatement();
    }

    protected virtual void CheckOptions()
    {
        
    }

    protected abstract string GetStatement();
}