using PlantUml.DiagramGenerators.Console;
using PlantUml.DiagramGenerators.Core;
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

async Task RenderFile(IPlantUmlRenderer renderer, string data, string file)
{
    var bytes = await renderer.RenderAsync(data, OutputFormat.Png);
    File.WriteAllBytes($"{file}.png", bytes);
}

var factory = new RendererFactory();
var renderer = factory.CreateRenderer(new PlantUmlSettings());

// State
await RenderFile(renderer, CreatePngFromJson(), "json");
await RenderFile(renderer, StateImageBuilder.CreateUmlCompositeState(), "uml_state_composite_state");
await RenderFile(renderer, StateImageBuilder.CreateUmlSubStateToSubState(), "uml_state_subState_to_subState");
await RenderFile(renderer, StateImageBuilder.CreateUmlLongName(), "uml_state_long_name");
await RenderFile(renderer, StateImageBuilder.CreateUmlFork(), "uml_state_fork");

//Sequence
await RenderFile(renderer, SequenceImageBuilder.CreateUmlSequenceWithColor(), "uml_sequence_with_color");
await RenderFile(renderer, SequenceImageBuilder.CreateSplittingDiagrams(), "uml_sequence_page_split");
await RenderFile(renderer, SequenceImageBuilder.CreateGroupingMessage(), "uml_sequence_grouping_message");
