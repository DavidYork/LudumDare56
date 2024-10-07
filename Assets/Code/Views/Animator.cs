using UnityEngine;
using UnityEngine.UI;

public class Animator: MonoBehaviour {
    [SerializeField] Sprite[] _frames;
    [SerializeField] Image _targetImage;

    int _randomOffsetFrame;

    void Awake() => _randomOffsetFrame = Random.Range(0, _frames.Length);

    void Update() {
        var frame = (Time.time / LD.Cfg.AnimTimePerFrame + _randomOffsetFrame) % _frames.Length;
        _targetImage.sprite = _frames[(int)frame];
    }
}