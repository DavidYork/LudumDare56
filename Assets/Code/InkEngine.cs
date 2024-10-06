using Ink.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
        Message = textBody.Replace("$t", "\t").TrimStart();
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
        _story.BindExternalFunction("chooseMapDestination", onChooseMapDestination);
        _story.BindExternalFunction("get", (InkList res) => onGet(res.ToEnum<Resource>()));
        _story.BindExternalFunction("gain", (InkList res, int amount) => onGain(res.ToEnum<Resource>(), amount));
        _story.BindExternalFunction("lose", (InkList res, int amount) => onLose(res.ToEnum<Resource>(), amount));
        _story.BindExternalFunction("showSummaryAndEndGame", onShowSummaryAndEndGame);
    }

    public void DoChoice(InkChoice command) {
        _story.ChooseChoiceIndex(command.ChoiceIndex);
        executeUntilNextChoice();
    }

    public void DoKnot(string knot) {
        _story.ChoosePathString(knot);
        executeUntilNextChoice();
    }

    public string[] GetAllEncounters() {
        var rv = new List<string>();
        var namedContent = _story.mainContentContainer.namedContent;
        foreach (var knot in namedContent) {
            if (knot.Value is Container) {
                rv.Add(knot.Key);
            }
        }
        return rv.ToArray();
    }

    public int GetVariable(string name) => (int)_story.variablesState[name];

    public void SetVariable(string name, int value) {
        Debug.Log($"Setting {name} to {value}");
        _story.variablesState[name] = value;
    }

    public bool KnotExists(string knotName) => _story.KnotContainerWithName(knotName) != null;

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

    void onChooseMapDestination() => Game.DoChooseMapDestination();

    void onShowSummaryAndEndGame() => Game.DoShowSummaryAndEndGame();

    int onGet(Resource res) => (res) switch {
        Resource.Coins => LD.Data.Coins,
        Resource.Dust => LD.Data.Dust,
        Resource.Fairies => LD.Data.Fairies,
        Resource.Fruit => LD.Data.Fruit,
        Resource.Trinkets => LD.Data.Trinkets,
        _ => throw new Exception($"Cannot understand {res}")
    };

    void onGain(Resource res, int amount) {
        switch (res) {
        case Resource.Coins:
            LD.Data.Coins += amount;
            UI.ResourcesMgr.Modify(Resource.Coins, amount);
            break;
        case Resource.Dust:
            LD.Data.Dust += amount;
            UI.ResourcesMgr.Modify(Resource.Dust, amount);
            break;
        case Resource.Fairies:
            LD.Data.Fairies += amount;
            UI.ResourcesMgr.Modify(Resource.Fairies, amount);
            break;
        case Resource.Fruit:
            LD.Data.Fruit += amount;
            UI.ResourcesMgr.Modify(Resource.Fruit, amount);
            break;
        case Resource.Trinkets:
            LD.Data.Trinkets += amount;
            UI.ResourcesMgr.Modify(Resource.Trinkets, amount);
            break;
        default: throw new ArgumentException($"Cannot handle {res}");
        }
    }

    void onLose(Resource res, int amount) => onGain(res, -amount);
}
