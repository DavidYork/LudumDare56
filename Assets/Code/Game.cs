using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Game {
    public static void DoChooseMapDestination() {
        SetState(GameData.GameState.ChoosingDestination);
    }

    public static void DoPlayerChoice(InkChoice choice) {
        LD.Ink.DoChoice(choice);
        UI.ChoiceMgr.RebuildChoices();
    }

    public static void DoSettleAndEndGame() {
        Debug.LogWarning("TODO: Settle and end game here");
        UI.ChoiceMgr.OnFinished = null;
        SetState(GameData.GameState.Settling);
    }

    public static void DoStartIntro() {
        LD.UI.ShowIntro();
    }

    public static void DoStartNewGame(string inkJson) {
        LD.OnStartNewGame(inkJson);
        UI.ResourcesMgr.Refresh();
        UI.MapMgr.OnStartNewGame();
        LD.Ink.DoKnot("Start");
        UI.ChoiceMgr.RebuildChoices();
        LD.UI.ShowGameplay();
    }

    public static void DoStartPrelude() {
        LD.UI.ShowPrelude();
    }

    public static void SetState(GameData.GameState newState) {
        LD.Data.State = newState;

        switch (newState) {
        case GameData.GameState.ChoosingDestination:
            UI.MapMgr.ShowValidPaths();
            break;
        case GameData.GameState.Traveling:
            break;
        case GameData.GameState.Encounter:
            UI.MapMgr.HidePaths();
            setupAndDoEncounter();
            break;
        case GameData.GameState.Explore:
            setupAndDoExploration();
            break;
        case GameData.GameState.FutureSight:
            break;
        case GameData.GameState.Settling:
            break;
        case GameData.GameState.Gameover:
            break;
        default:
            throw new System.Exception($"Cannot understand {newState}");
        }
    }

    // Private

    static void setupAndDoEncounter() {
        var valids = UI.MapMgr.CurrentPOI.Encounters;
        var poi = UI.MapMgr.CurrentPOI;

        if (valids.Length == 0) {
            Debug.LogError($"No valid encounter types at {poi.name}. Configure POI on map.");
            SetState(GameData.GameState.ChoosingDestination);
            return;
        }

        var allEncounters = LD.Ink.GetAllEncounters();
        var validEncounters = new List<string>();
        foreach (var valid in valids) {
            foreach (var encounter in allEncounters) {
                var prefix = $"Encounter_{valid}_";
                if (encounter.StartsWith(prefix)) {
                    validEncounters.Add(encounter);
                }
            }
        }

        if (validEncounters.Count == 0) {
            Debug.LogError($"Zero valid encounters found for {poi.name} - add more Ink content or configure on map");
            SetState(GameData.GameState.ChoosingDestination);
            return;
        }

        var unvisited = new HashSet<string>();
        foreach (var candidate in validEncounters) {
            if (!LD.Data.VisitedEncounters.Contains(candidate)) {
                unvisited.Add(candidate);
            }
        }

        string chosenEncounter;
        if (unvisited.Count == 0) {
            chosenEncounter = validEncounters[UnityEngine.Random.Range(0, validEncounters.Count)];
        } else {
            chosenEncounter = unvisited.ToArray()[UnityEngine.Random.Range(0, unvisited.Count)];
            LD.Data.VisitedEncounters.Add(chosenEncounter);
        }

        LD.Ink.DoKnot(chosenEncounter);
        UI.ChoiceMgr.OnFinished = () => SetState(GameData.GameState.Explore);
        UI.ChoiceMgr.RebuildChoices();
    }

    static void setupAndDoExploration() {
        var poi = UI.MapMgr.CurrentPOI;
        var locationKnot = $"Location_{poi.name}";
        if (!LD.Ink.KnotExists(locationKnot)) {
            Debug.LogError($"Knot does not exist: {locationKnot}. Write it in Ink.");
            SetState(GameData.GameState.ChoosingDestination);
            return;
        }

        Func<int, int, int, int> calcVal = (low, high, stories)
            => UnityEngine.Random.Range(low, high+1) + stories;

        LD.Ink.SetVariable($"{LocAttributes.animals}", calcVal(poi.LowAnimals, poi.HighAnimals, LD.Data.AnimalStories));
        LD.Ink.SetVariable($"{LocAttributes.plants}", calcVal(poi.LowPlants, poi.HighPlants, LD.Data.PlantStories));
        LD.Ink.SetVariable($"{LocAttributes.magic}", calcVal(poi.LowMagic, poi.HighMagic, LD.Data.MagicStories));
        LD.Ink.SetVariable($"{LocAttributes.beauty}", calcVal(poi.LowBeauty, poi.HighBeauty, LD.Data.BeautyStories));

        LD.Ink.DoKnot(locationKnot);
        UI.ChoiceMgr.RebuildChoices();
        UI.ChoiceMgr.OnFinished = () => {
            SetState(GameData.GameState.ChoosingDestination);
        };
    }
}
