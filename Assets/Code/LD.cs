using UnityEngine;

//----------------------------------------------------------------------------------------------------------------------

public static class LD {
    public static GameData Data;
    public static InkEngine Ink;

    public static void Initialize(string inkJson) {
        Data = new GameData();
        Ink = new InkEngine(inkJson);
    }
}