using TMPro;
using UnityEngine;

public class ChoiceButtonView: MonoBehaviour {
    [SerializeField] TextMeshProUGUI _caption;

    public InkChoice Choice { get; private set; }

    public void OnClick() {
        Game.DoPlayerChoice(Choice);
        LD.Audio.PlaySound(Sound.click);
    }

    public void Setup(InkChoice choice) {
        Choice = choice;
        _caption.text = choice.Description;
    }
}