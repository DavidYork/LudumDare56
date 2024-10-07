using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager: MonoBehaviour {
    [SerializeField] ChoiceButtonView[] _choiceButtonsLeftAndRight;
    [SerializeField] ChoiceButtonView[] _choiceButtonsCenter;
    [SerializeField] Image _illustration;
    [SerializeField] TextMeshProUGUI[] _contentBody;
    [SerializeField] GameObject _contentAreaWide;
    [SerializeField] GameObject _contentAreaShort;
    [SerializeField] GameObject _massiveTextWindow;
    [SerializeField] GameObject[] _choiceAnchors;
    [SerializeField] GameObject _hideMapOverlay;

    public Action OnFinished;

    public void ClearChoices() {
        _hideMapOverlay.SetActive(false);
        foreach (var choice in _choiceButtonsCenter) {
            choice.gameObject.SetActive(false);
        }
        foreach (var choice in _choiceButtonsLeftAndRight) {
            choice.gameObject.SetActive(false);
        }

        _contentAreaWide.gameObject.SetActive(false);
        _contentAreaShort.gameObject.SetActive(false);
        _illustration.gameObject.SetActive(false);
    }

    public void OnTheEnd() {
        LD.Audio.PlaySound(Sound.click);
        var callback = OnFinished;
        OnFinished = null;
        callback?.Invoke();
        UI.Fairies.Hide();
    }

    public void RebuildChoices() {
        ClearChoices();

        var inkState = LD.Ink.CurrentState;
        if (inkState.Message.IsNil()) {
            var callback = OnFinished;
            OnFinished = null;
            callback?.Invoke();
            return;
        }

        _hideMapOverlay.SetActive(true);
        var choices = inkState.Choices;
        var choiceButtons = choices.Length > _choiceButtonsCenter.Length
            ? _choiceButtonsLeftAndRight : _choiceButtonsCenter;

        if (choices.Length > choiceButtons.Length) {
            Debug.LogError($"Too many choices: {choices.Length}");
        }

        for (var i=0; i<choiceButtons.Length; i++) {
            if (i < choices.Length) {
                choiceButtons[i].Setup(choices[i]);
                choiceButtons[i].gameObject.SetActive(true);
            } else {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        var showIllustration = inkState.Tags.TryGetValue("art", out string artName);
        if (showIllustration) {
            _illustration.gameObject.SetActive(true);
            _illustration.sprite = Resources.Load<Sprite>($"Illustrations/{artName}");
        }

        foreach (var cb in _contentBody) {
            cb.text = inkState.Message;
        }

        var targetContent = showIllustration ? _contentAreaShort : _contentAreaWide;
        if (!_massiveTextWindow.activeSelf) {
            targetContent.SetActive(true);
        }

        if (choiceButtons.Length == 0) {
            Debug.LogError($"Path has zero choices but still is showing content");
            if (OnFinished != null) {
                OnTheEnd();
                ClearChoices();
            } else {
                ClearChoices();
                // Hack to deal with this extremely awful situtation that can be created by ink content
                // not including any choices for the player
                Game.DoChooseMapDestination();
            }
        }
    }

    public void ShowMassiveTextWindow(bool visible) {
        _hideMapOverlay.SetActive(true);
        _massiveTextWindow.SetActive(visible);
        if (visible) {
            _contentAreaShort.gameObject.SetActive(false);
            _contentAreaWide.gameObject.SetActive(false);
        }
    }

    // Private

    void Awake() {
        foreach (var anchor in _choiceAnchors) {
            anchor.SetActive(true);
        }
    }
}