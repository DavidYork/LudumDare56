using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResourceView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] TextMeshProUGUI _value;
    [SerializeField] Resource _resource;

    float _startWiggleTimestamp;
    Color _wiggleColor;

    public void OnPointerEnter(PointerEventData eventData) {
        UI.Tooltip.Show(_resource);
    }

    public void OnPointerExit(PointerEventData eventData) {
        UI.Tooltip.Hide();
    }

    public void Set(int val) => _value.text = $"{name}\n{val}";

    public void Wiggle(int amount) {
        if (amount == 0) {
            return;
        }

        _startWiggleTimestamp = Time.time;
        _wiggleColor = amount >= 0 ? LD.Cfg.GfxResourceGoodColor : LD.Cfg.GfxResourceBadColor;
    }

    // Private

    void Update() {
        if (_startWiggleTimestamp == 0) {
            return;
        }

        var duration = LD.Cfg.GfxResourceWiggleDuration;
        if (Time.time >= _startWiggleTimestamp + duration) {
            _startWiggleTimestamp = 0;
            _value.color = Color.white;
            return;
        }

        var elapsed = Time.time - _startWiggleTimestamp;
        _value.color = Color.Lerp(_wiggleColor, Color.white, elapsed / duration);
    }
}
