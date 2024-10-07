using System;
using UnityEngine;

public class Bootstrapper: MonoBehaviour {
    public enum GameStart {
        Gameplay, Intro, Prelude
    }

    public TextAsset InkCompiledJson;

    [SerializeField] GameStart _gameStartState;

    public void Start() {
        LD.Initialize();

        if (_gameStartState == GameStart.Intro || !Application.isEditor) {
            Game.DoStartIntro();
        } else if (_gameStartState == GameStart.Gameplay) {
            Game.DoStartNewGame(InkCompiledJson.ToString());
        } else if (_gameStartState == GameStart.Prelude) {
            Game.DoStartPrelude();
        } else {
            throw new Exception($"Invalid game start state {_gameStartState}");
        }

        LD.Audio.Muted = PlayerPrefs.GetInt("muted", 0) == 1;
        LD.Audio.PlayAmbient(Ambient.heart_of_sin);
    }
}
