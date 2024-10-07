using UnityEngine;
using UnityEngine.UI;

public class IntroView: MonoBehaviour {
    [SerializeField] Image _muteButtonImage;
    [SerializeField] Sprite _mutedOnImage;
    [SerializeField] Sprite _mutedOffImage;

    public void OnStartNewGame() {
        Game.DoStartNewGame(LD.Boots.InkCompiledJson.ToString());
        LD.Audio.PlaySound(Sound.click);
    }

    public void OnToggleMute() {
        var muted = PlayerPrefs.GetInt("muted", 0) == 1;
        muted = !muted;
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
        LD.Audio.Muted = muted;
        refreshMuteButton();
        LD.Audio.PlaySound(Sound.click);
    }

    // Private

    void refreshMuteButton() {
        var muted = PlayerPrefs.GetInt("muted", 0) == 1;
        _muteButtonImage.sprite = muted ? _mutedOnImage : _mutedOffImage;
    }

    void Awake() => refreshMuteButton();
}