using PlantUml.DiagramGenerators.Core;
using PlantUml.DiagramGenerators.Uml.Sequence;
using PlantUml.DiagramGenerators.Uml.State;
using PlantUml.Net;

string CreatePngFromJson()
{
    var json = @"@startjson
{
   ""fruit"":""Apple"",
   ""size"":""%PLACEHOLDER%"",
   ""color"": [""Red"", ""Green""]
}
@endjson";

    return json.Replace("%PLACEHOLDER%", BuiltInFunctions.Darken("#FF0808", 50));
}

string CreateUmlCompositeState()
{
    return new StateDiagramBuilder().AddStartTransition("NotShooting")
        .AddSubState("NotShooting", b =>
        {
            b.AddStartTransition("Idle")
                .AddStateTransition("Idle", "Configuring", "EvConfig")
                .AddStateTransition("Configuring", "Idle", "EvConfig");
        })
        .AddSubState("Configuring", b =>
        {
            b.AddStartTransition("NewValueSelection")
                .AddStateTransition("NewValueSelection", "NewValuePreview", "EvNewValue")
                .AddStateTransition("NewValuePreview", "NewValueSelection", "EvNewValueRejected")
                .AddStateTransition("NewValuePreview", "NewValueSelection", "EvNewValueSaved")
                .AddSubState("NewValuePreview", b =>
                {
                    b.AddStateTransition("State1", "State2");
                });
        })
        .Build();
}

string CreateUmlSubStateToSubState()
{
    return new StateDiagramBuilder().AddSubState("A", b =>
        {
            b.AddSubState("X", b => { })
                .AddSubState("Y", b => { });
        })
        .AddSubState("B", b =>
        {
            b.AddSubState("Z", b => { });
        })
        .AddStateTransition("X", "Z")
        .AddStateTransition("Z", "Y")
        .Build(options =>
        {
            options.HideEmptyDescriptionTag = false;
        });
}

string CreateUmlLongName()
{
    return new StateDiagramBuilder()
        .AddStartTransition("State1")
        .AddStateTransition("State1", "State2", "Succeeded")
        .AddFinalTransition("State1", "Aborted")
        .AddStateTransition("State2", "State3", "Succeeded")
        .AddFinalTransition("State2", "Aborted")
        .AddSubState("State3", b =>
        {
            b.AddState(new StateOptions("Accumulate Enough Data\\nLong State Name")
                {
                    Alias = "long1",
                    Description = "Just a test"
                })
                .AddStartTransition("long1")
                .AddStateTransition("long1", "long1", "New Data")
                .AddStateTransition("long1", "ProcessData", "Enough Data");
        })
        .AddStateTransition("State3", "State3", "Failed")
        .AddFinalTransition("State3", "Success / Save Result")
        .AddFinalTransition("State3", "Aborted")
        .Build(options =>
        {
            options.AddOptions("scale 600 width");
        });
}

string CreateUmlFork()
{
    return new StateDiagramBuilder()
        .AddState(new StateOptions("fork_state")
        {
            Alias = "f1",
            Type = StateType.Fork
        })
        .AddStartTransition("f1")
        .AddStateTransition("f1", "State2")
        .AddStateTransition("f1", "State3")
        .AddState(new StateOptions("join_state")
        {
            Alias = "f2",
            Type = StateType.Join
        })
        .AddStateTransition("State2", "f2", "Succeeded")
        .AddStateTransition("State3", "f2", "Aborted")
        .AddStateTransition("f2", "State4")
        .AddFinalTransition("State4")
        .Build();
}

string CreateUmlSequenceWithColor()
{
    return new SequenceDiagramBuilder()
        .AddParticipant(ParticipantOptions.CreateActor("Bob").WithColor("#red"))
        .AddParticipant(ParticipantOptions.CreateParticipant("Alice"))
        .AddParticipant(ParticipantOptions.CreateParticipant("I have a really\\nlong name", "L").WithColor("#99FF99"))
        .AddSequence("Alice", "Bob", "Authentication Request")
        .AddSequence("Bob", "Alice", "Authentication Response")
        .AddSequence("Bob", "L", "Log transaction")
        .Build();
}

async Task RenderFile(IPlantUmlRenderer renderer, string data, string file)
{
    var bytes = await renderer.RenderAsync(data, OutputFormat.Png);
    File.WriteAllBytes($"{file}.png", bytes);
}

var factory = new RendererFactory();
var renderer = factory.CreateRenderer(new PlantUmlSettings());

await RenderFile(renderer, CreatePngFromJson(), "json");
await RenderFile(renderer, CreateUmlCompositeState(), "uml_composite_state");
await RenderFile(renderer, CreateUmlSubStateToSubState(), "uml_subState_to_subState");
await RenderFile(renderer, CreateUmlLongName(), "uml_long_name");
await RenderFile(renderer, CreateUmlFork(), "uml_fork");

await RenderFile(renderer, CreateUmlSequenceWithColor(), "uml_sequence_with_color");
