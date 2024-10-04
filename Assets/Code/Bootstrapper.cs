using UnityEngine;

public class Bootstrapper: MonoBehaviour {
    public TextAsset InkCompiledJson;

    public void Start() {
        LD.Initialize();
        Game.DoStartNewGame(InkCompiledJson.ToString());
    }
}
