using UnityEngine;

public class UI: MonoBehaviour {
    public static ChoiceManager ChoiceMgr { get; private set; }

    public void Awake() {
        ChoiceMgr = GameObject.FindFirstObjectByType<ChoiceManager>();
    }
}
