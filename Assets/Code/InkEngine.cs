using Ink.Runtime;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public readonly struct InkChoice {
    public readonly string Description => _choice.text;
    public readonly int ChoiceIndex => _choice.index;
    public readonly string[] Tags => _choice?.tags.ToArray() ?? new string[0];

    readonly Choice _choice;

    public InkChoice(Choice choice) {
        _choice = choice;
    }
}

//----------------------------------------------------------------------------------------------------------------------

public readonly struct InkEngineState {
    public readonly string Message;
    public readonly InkChoice[] Choices;
    public readonly string ImageName;
    public readonly Dictionary<string, string> Tags;
    public readonly string[] Metadata;

    public InkEngineState(string textBody, Story story, string[] metadata,
        Dictionary<string, string> tags)
    {
        Message = textBody;
        Choices = story.currentChoices.ConvertAll(s => new InkChoice(s)).ToArray();
        ImageName = null;
        Tags = tags;
        Metadata = metadata;
    }
}

//----------------------------------------------------------------------------------------------------------------------

public class InkEngine {
    Story _story;

    public InkEngineState CurrentState { get; private set; }

    public InkEngine(string inkJson) {
        _story = new Story(inkJson);
    }

    public void DoChoice(InkChoice command) {
        _story.ChooseChoiceIndex(command.ChoiceIndex);
        executeUntilNextChoice();
    }

    public void DoKnot(string knot) {
        _story.ChoosePathString(knot);
        executeUntilNextChoice();
    }

    // Private

    void executeUntilNextChoice() {
        var sb = new StringBuilder();
        var tags = new Dictionary<string, string>();
        var meta = new HashSet<string>();

        while (_story.canContinue) {
            sb.Append(_story.Continue());
            foreach (var tag in _story.currentTags) {
                if (tag.Contains(":")) {
                    var splits = tag.Split(':');
                    var key = splits[0].Trim();
                    var val = splits[1].Trim();
                    tags[key] = val;
                } else {
                    meta.Add(tag.Trim());
                }
            }
        }

        CurrentState = new InkEngineState(sb.ToString(), _story, meta.ToArray(), tags);
    }
}
