using PlantUml.DiagramGenerators.Uml.Sequence;

namespace PlantUml.DiagramGenerators.Console;

internal static class SequenceImageBuilder
{
    internal static string CreateUmlSequenceWithColor()
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

    internal static string CreateSplittingDiagrams()
    {
        return new SequenceDiagramBuilder()
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 1"))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 2", ignoreAutomaticArrowDirectionDetection: true))
            .AddNewPage()
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 3", ignoreAutomaticArrowDirectionDetection: true))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 4", ignoreAutomaticArrowDirectionDetection: true))
            .AddNewPage("A title for the\\nlast page")
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 5", ignoreAutomaticArrowDirectionDetection: true))
            .AddSequence(new SequenceOptions("Alice", "Bob", "message 6", ignoreAutomaticArrowDirectionDetection: true))
            .Build();
    }

    internal static string CreateGroupingMessage()
    {
        return new SequenceDiagramBuilder()
            .AddSequence(new SequenceOptions("Alice", "Bob", "Authentication Request"))
            .AddMessageGroup(gb =>
            {
                gb.AddAlt("successful case",
                        builder => { builder.AddSequence(new SequenceOptions("Bob", "Alice", "Authentication Accepted")); })
                    .AddElse("some kind of failure", builder =>
                    {
                        builder.AddSequence(new SequenceOptions("Bob", "Alice", "Authentication Failure"));
                        builder.AddGroup("My own label", subBuilder =>
                        {
                            subBuilder
                                .AddSequence(new SequenceOptions("Alice", "Log", "Log attack start"))
                                .AddLoop(1000,
                                    subSubBuilder =>
                                    {
                                        subSubBuilder.AddSequence(new SequenceOptions("Alice", "Bob", "DNS Attack"));
                                    })
                                .AddSequence(new SequenceOptions("Alice", "Log", "Log attack end",
                                    ignoreAutomaticArrowDirectionDetection: true));
                        });
                    })
                    .AddElse("Another type of failure",
                        builder => { builder.AddSequence(new SequenceOptions("Bob", "Alice", "Please repeat")); })
                    .AddEnd();
            })
            .Build();
    }

    internal static string CreateReference()
    {
        var alice = ParticipantOptions.CreateParticipant("Alice");
        var bob = ParticipantOptions.CreateActor("Bob");

        return new SequenceDiagramBuilder()
            .AddParticipant(alice)
            .AddParticipant(bob)
            .AddReference("init", alice, bob)
            .AddSequence(alice, bob, "hello")
            .AddReference(@"This can be on
several lines", bob)
            .Build();
    }
}
