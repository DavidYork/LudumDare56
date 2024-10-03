using UnityEngine;

public class Bootstrapper: MonoBehaviour {
    public TextAsset InkCompiledJson;

    public void Awake() {
        LD.Initialize(InkCompiledJson.ToString());
    }
}
