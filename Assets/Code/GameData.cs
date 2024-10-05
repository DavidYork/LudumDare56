using System;
using System.Collections.Generic;

[Serializable]
public class GameData {
    public enum GameState {
        ChoosingDestination,    // Deciding where to go next
        Traveling,              // Animating the colony moving
        Encounter,              // In an encounter
        Explore,                // Exploring an area (post-encounter)
        FutureSight,            // Using a coin (or not, short msg then) to investigate a location for settling
        Settling,               // Spewing information about the location to the player
        Gameover                // Final end game screen
    }

    public GameState State = GameState.ChoosingDestination;

    public int TimePassed = 0;
    public int Coins = 10;
    public int Dust = 100;
    public int Fairies = 100;
    public int Fruit = 100;
    public int Stories => AnimalStories + PlantStories + MagicStories + BeautyStories;
    public int Trinkets = 100;

    public int AnimalStories = 0;
    public int PlantStories = 0;
    public int MagicStories = 0;
    public int BeautyStories = 0;

    public HashSet<string> VisitedEncounters = new HashSet<string>();
}