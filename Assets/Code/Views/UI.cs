using UnityEngine;

public class UI: MonoBehaviour {
    [SerializeField] GameObject _gameplayAnchor;
    [SerializeField] GameObject _introAnchor;
    [SerializeField] GameObject _preludeAnchor;

    public static ChoiceManager ChoiceMgr { get; private set; }
    public static ResourcesManager ResourcesMgr { get; private set; }
    public static MapManager MapMgr { get; private set; }
    public static Tooltip Tooltip { get; private set; }
    public static FairyParty Fairies { get; private set; }

    public void Awake() {
        ChoiceMgr = GameObject.FindFirstObjectByType<ChoiceManager>(FindObjectsInactive.Include);
        ResourcesMgr = GameObject.FindFirstObjectByType<ResourcesManager>(FindObjectsInactive.Include);
        MapMgr = GameObject.FindFirstObjectByType<MapManager>(FindObjectsInactive.Include);
        Tooltip = GameObject.FindFirstObjectByType<Tooltip>(FindObjectsInactive.Include);
        Fairies = GameObject.FindFirstObjectByType<FairyParty>(FindObjectsInactive.Include);
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
        Tooltip.Hide();
    }
}
