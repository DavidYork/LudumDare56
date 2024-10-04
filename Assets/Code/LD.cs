using UnityEngine;

//----------------------------------------------------------------------------------------------------------------------

public static class LD {
    public static GameData Data;
    public static InkEngine Ink;
    public static DB DB;

    public static void Initialize() {
    }

    public static void OnStartNewGame(string inkJson) {
        Ink = new InkEngine(inkJson);
        Data = new GameData();
        DB = new DB();

        var s = DB.Illustration("Test 1");
        var j = DB.TestJson("icecream");
    }
}