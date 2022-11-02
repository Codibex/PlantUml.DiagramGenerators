using PlantUml.DiagramGenerators.Core;
using PlantUml.DiagramGenerators.Uml.Sequence;
using PlantUml.DiagramGenerators.Uml.Status;
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
    return new StatusDiagramBuilder().AddStartTransition("NotShooting")
        .AddSubStatus("NotShooting", b =>
        {
            b.AddStartTransition("Idle")
                .AddStatusTransition("Idle", "Configuring", "EvConfig")
                .AddStatusTransition("Configuring", "Idle", "EvConfig");
        })
        .AddSubStatus("Configuring", b =>
        {
            b.AddStartTransition("NewValueSelection")
                .AddStatusTransition("NewValueSelection", "NewValuePreview", "EvNewValue")
                .AddStatusTransition("NewValuePreview", "NewValueSelection", "EvNewValueRejected")
                .AddStatusTransition("NewValuePreview", "NewValueSelection", "EvNewValueSaved")
                .AddSubStatus("NewValuePreview", b =>
                {
                    b.AddStatusTransition("State1", "State2");
                });
        })
        .Build();
}

string CreateUmlSubStateToSubState()
{
    return new StatusDiagramBuilder().AddSubStatus("A", b =>
        {
            b.AddSubStatus("X", b => { })
                .AddSubStatus("Y", b => { });
        })
        .AddSubStatus("B", b =>
        {
            b.AddSubStatus("Z", b => { });
        })
        .AddStatusTransition("X", "Z")
        .AddStatusTransition("Z", "Y")
        .Build(options =>
        {
            options.HideEmptyDescriptionTag = false;
        });
}

string CreateUmlLongName()
{
    return new StatusDiagramBuilder()
        .AddStartTransition("State1")
        .AddStatusTransition("State1", "State2", "Succeeded")
        .AddFinalTransition("State1", "Aborted")
        .AddStatusTransition("State2", "State3", "Succeeded")
        .AddFinalTransition("State2", "Aborted")
        .AddSubStatus("State3", b =>
        {
            b.AddStatus(new StatusOptions("Accumulate Enough Data\\nLong State Name")
                {
                    Alias = "long1",
                    Description = "Just a test"
                })
                .AddStartTransition("long1")
                .AddStatusTransition("long1", "long1", "New Data")
                .AddStatusTransition("long1", "ProcessData", "Enough Data");
        })
        .AddStatusTransition("State3", "State3", "Failed")
        .AddFinalTransition("State3", "Success / Save Result")
        .AddFinalTransition("State3", "Aborted")
        .Build(options =>
        {
            options.AddOptions("scale 600 width");
        });
}

string CreateUmlFork()
{
    return new StatusDiagramBuilder()
        .AddStatus(new StatusOptions("fork_state")
        {
            Alias = "f1",
            Type = StatusType.Fork
        })
        .AddStartTransition("f1")
        .AddStatusTransition("f1", "State2")
        .AddStatusTransition("f1", "State3")
        .AddStatus(new StatusOptions("join_state")
        {
            Alias = "f2",
            Type = StatusType.Join
        })
        .AddStatusTransition("State2", "f2", "Succeeded")
        .AddStatusTransition("State3", "f2", "Aborted")
        .AddStatusTransition("f2", "State4")
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
