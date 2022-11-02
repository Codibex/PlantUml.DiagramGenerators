namespace PlantUml.DiagramGenerators.Uml.Status;
internal class StatusTransitionStatementBuilder : StatementBuilderBase<StatusTransitionOptions>
{
    internal StatusTransitionStatementBuilder(StatusTransitionOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        return Options.TransitionType switch
        {
            TransitionType.Start => GetStartTransition(),
            TransitionType.Standard => GetStatusTransition(),
            TransitionType.Final => GetFinalTransition(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string GetStartTransition()
    {
        string targetStatusName = new StatusStatementBuilder(Options.SourceStatusOptions).Build();
        return AppendDescription($"[*] {GetArrow()} {targetStatusName}", Options.TransitionDescription);
    }

    private string GetStatusTransition()
    {
        string sourceStatusName = new StatusStatementBuilder(Options.SourceStatusOptions).Build();
        string targetStatusName = new StatusStatementBuilder(Options.TargetStatusOptions!).Build();
        return AppendDescription($"{sourceStatusName} {GetArrow()} {targetStatusName}", Options.TransitionDescription);
    }

    private string GetFinalTransition()
    {
        string sourceStatusName = new StatusStatementBuilder(Options.SourceStatusOptions).Build();
        return AppendDescription($"{sourceStatusName} {GetArrow()} [*]", Options.TransitionDescription);
    }

    private string GetArrow()
    {
        return new ArrowStatementBuilder().Build(Options.ArrowConfig);
    }

    private static string AppendDescription(string transition, string? transitionDescription)
        => string.IsNullOrWhiteSpace(transitionDescription) ? transition : $"{transition} : {transitionDescription}";

    
}
