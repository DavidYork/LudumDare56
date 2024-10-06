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

    public Action OnFinished;

    public void ClearChoices() {
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
        var callback = OnFinished;
        OnFinished = null;
        callback?.Invoke();
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
    }

    public void ShowMassiveTextWindow(bool visible) {
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