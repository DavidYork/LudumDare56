using UnityEngine;

//----------------------------------------------------------------------------------------------------------------------

public static class LD {
    public static GameData Data;
    public static InkEngine Ink;

    public static void Initialize() {
    }

    public static void OnStartNewGame(string inkJson) {
        Ink = new InkEngine(inkJson);
        Data = new GameData();
    }
}