using UnityEngine;

public class IntroView: MonoBehaviour {
    public void OnStartNewGame() => Game.DoStartNewGame(LD.Boots.InkCompiledJson.ToString());
}