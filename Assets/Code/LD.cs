using UnityEngine;

//----------------------------------------------------------------------------------------------------------------------

public static class LD {
    public static GameData Data;
    public static InkEngine Ink;
    public static DB DB;
    public static AudioManager Audio;
    public static UI UI;
    public static Config Cfg;

    public static void Initialize() {
        Audio = GameObject.FindFirstObjectByType<AudioManager>();
        if (Audio == null) {
            var go = new GameObject("Audio Manager");
            Audio = go.AddComponent<AudioManager>();
        }
        UI = GameObject.FindFirstObjectByType<UI>();
        Cfg = GameObject.FindFirstObjectByType<Config>();
    }

    public static void OnStartNewGame(string inkJson) {
        Ink = new InkEngine(inkJson);
        Data = new GameData();
        DB = new DB();
    }
}
