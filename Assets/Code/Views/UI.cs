using UnityEngine;

public class UI: MonoBehaviour {
    [SerializeField] GameObject _gameplayAnchor;
    [SerializeField] GameObject _introAnchor;
    [SerializeField] GameObject _preludeAnchor;

    public static ChoiceManager ChoiceMgr { get; private set; }
    public static ResourcesManager ResourcesMgr { get; private set; }
    public static MapManager MapMgr { get; private set; }

    public void Awake() {
        ChoiceMgr = GameObject.FindFirstObjectByType<ChoiceManager>();
        ResourcesMgr = GameObject.FindFirstObjectByType<ResourcesManager>();
        MapMgr = GameObject.FindFirstObjectByType<MapManager>();
    }

    public void ShowIntro() => _introAnchor.SetActive(true);

    public void ShowGameplay() {
        _gameplayAnchor.SetActive(true);
        ResourcesMgr.Refresh();
    }

    public void ShowPrelude() => _preludeAnchor.SetActive(true);

    public void HideAll() {
        _gameplayAnchor.SetActive(false);
        _introAnchor.SetActive(false);
        _preludeAnchor.SetActive(false);
    }
}
