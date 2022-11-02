namespace PlantUml.DiagramGenerators.Uml.Sequence;

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
        return GetStatement();
    }

    protected abstract string GetStatement();
}