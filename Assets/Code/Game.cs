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

    public static void DoShowSummaryAndEndGame() {
        UI.ChoiceMgr.OnFinished = () => SetState(GameData.GameState.Gameover);
        UI.ChoiceMgr.ShowMassiveTextWindow(true);
        UI.Fairies.Show(LD.Data.Fairies / 50);
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
        UI.ChoiceMgr.ShowMassiveTextWindow(false);
        UI.ChoiceMgr.RebuildChoices();
        LD.UI.HideAll();
        LD.UI.ShowGameplay();
    }

    public static void DoStartPrelude() {
        LD.UI.ShowPrelude();
    }

    public static void SetState(GameData.GameState newState) {
        Debug.Log($"State changed from {LD.Data.State} to {newState} ");
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
            Game.DoStartIntro();
            break;
        default:
            throw new System.Exception($"Cannot understand {newState}");
        }
    }

    // Private

    static void setupAndDoEncounter() {
        var poi = UI.MapMgr.CurrentPOI;

        LD.Ink.SetVariable("animalName", poi.Animals);
        LD.Ink.SetVariable("plantName", poi.Plants);
        LD.Ink.SetVariable("badPlantName", poi.BadPlants);
        LD.Ink.SetVariable("locationName", poi.FriendlyName);
        LD.Ink.SetVariable("locationDescription", poi.Description);

        var valids = poi.Encounters;
        if (valids.Length == 0) {
            Debug.LogError($"No valid encounter types at {poi.FriendlyName}. Configure POI on map.");
            SetState(GameData.GameState.Explore);
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
            SetState(GameData.GameState.Explore);
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

        var strippedEncounter = chosenEncounter.Replace("Encounter_", "");
        var splitIdx = chosenEncounter.IndexOf("_") + 1;
        var encounterName = chosenEncounter.Substring(splitIdx, chosenEncounter.Length - splitIdx).Replace("_", " ");
        Debug.Log($"Doing encounter {encounterName}");
        LD.Ink.SetVariable("encounterName", encounterName);
        LD.Ink.DoKnot(chosenEncounter);
        UI.ChoiceMgr.OnFinished = () => SetState(GameData.GameState.Explore);
        UI.ChoiceMgr.RebuildChoices();
    }

    static void setupAndDoExploration() {
        var poi = UI.MapMgr.CurrentPOI;
        if (poi.SkipThisLocation) {
            SetState(GameData.GameState.ChoosingDestination);
            return;
        }

        var locationKnot = $"Location_{poi.KnotName}";
        if (!LD.Ink.KnotExists(locationKnot)) {
            Debug.LogError($"Knot does not exist: {locationKnot}. Write it in Ink.");
            SetState(GameData.GameState.ChoosingDestination);
            return;
        }

        Func<int, int, int, int> calcVal = (low, high, stories)
            => UnityEngine.Random.Range(low, high+1) + stories;

        LD.Ink.SetVariable($"{LocAttributes.Animals}".ToLower(),
            calcVal(poi.LowAnimals, poi.HighAnimals, LD.Data.AnimalStories));

        LD.Ink.SetVariable($"{LocAttributes.Plants}".ToLower(),
            calcVal(poi.LowPlants, poi.HighPlants, LD.Data.PlantStories));

        LD.Ink.SetVariable($"{LocAttributes.Magic}".ToLower(),
            calcVal(poi.LowMagic, poi.HighMagic, LD.Data.MagicStories));

        LD.Ink.SetVariable($"{LocAttributes.Beauty}".ToLower(),
            calcVal(poi.LowBeauty, poi.HighBeauty, LD.Data.BeautyStories));


        LD.Ink.DoKnot(locationKnot);
        UI.ChoiceMgr.RebuildChoices();
        UI.ChoiceMgr.OnFinished = () => {
            SetState(GameData.GameState.ChoosingDestination);
        };
    }
}
