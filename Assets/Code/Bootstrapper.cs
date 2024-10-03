using UnityEngine;

public class Bootstrapper: MonoBehaviour {
    public TextAsset InkCompiledJson;

    public void Awake() => LD.Initialize();

    public void Start() => Game.DoStartNewGame(InkCompiledJson.ToString());
}
