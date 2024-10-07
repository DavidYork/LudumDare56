using UnityEngine;
using UnityEngine.UI;

public class Animator: MonoBehaviour {
    [SerializeField] Sprite[] _frames;
    [SerializeField] Image _targetImage;

    void Update() {
        var frame = (Time.time / LD.Cfg.AnimTimePerFrame) % _frames.Length;
        _targetImage.sprite = _frames[(int)frame];
    }
}