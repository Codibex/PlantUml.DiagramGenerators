namespace PlantUml.DiagramGenerators.Uml.State;
internal class StateTransitionStatementBuilder : StatementBuilderBase<StateTransitionOptions>
{
    internal StateTransitionStatementBuilder(StateTransitionOptions options) : base(options)
    {
    }

    protected override string GetStatement()
    {
        return Options.TransitionType switch
        {
            TransitionType.Start => GetStartTransition(),
            TransitionType.Standard => GetStateTransition(),
            TransitionType.Final => GetFinalTransition(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private string GetStartTransition()
    {
        string targetStateName = new StateStatementBuilder(Options.SourceStateOptions).Build();
        return AppendDescription($"[*] {GetArrow()} {targetStateName}", Options.TransitionDescription);
    }

    private string GetStateTransition()
    {
        string sourceStateName = new StateStatementBuilder(Options.SourceStateOptions).Build();
        string targetStateName = new StateStatementBuilder(Options.TargetStateOptions!).Build();
        return AppendDescription($"{sourceStateName} {GetArrow()} {targetStateName}", Options.TransitionDescription);
    }

    private string GetFinalTransition()
    {
        string sourceStateName = new StateStatementBuilder(Options.SourceStateOptions).Build();
        return AppendDescription($"{sourceStateName} {GetArrow()} [*]", Options.TransitionDescription);
    }

    private string GetArrow()
    {
        return new ArrowStatementBuilder().Build(Options.ArrowConfig);
    }

    private static string AppendDescription(string transition, string? transitionDescription)
        => string.IsNullOrWhiteSpace(transitionDescription) ? transition : $"{transition} : {transitionDescription}";

    
}
