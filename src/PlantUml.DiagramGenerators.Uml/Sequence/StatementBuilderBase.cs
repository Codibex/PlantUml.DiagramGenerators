namespace PlantUml.DiagramGenerators.Uml.Sequence;

internal abstract class StatementBuilderBase<TOptions> 
    where TOptions : new()
{
    protected TOptions Options { get; } = new();

    internal string Build(Action<TOptions>? config = null)
    {
        config?.Invoke(Options);
        return GetStatement();
    }

    protected abstract string GetStatement();
}