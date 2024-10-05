using System;
using UnityEngine;
using UnityEngine.UI;

public class PathView: MonoBehaviour {
    public enum PathState { Hidden, Visible, Emphasized }

    public POIView Start;
    public POIView End;
    public Image[] PathDots;

    PathState _state;

    public PathState State {
        get => _state;
        set {
            _state = value;
            switch (_state) {
            case PathState.Hidden:
                foreach (var pd in PathDots) {
                    pd.gameObject.SetActive(false);
                }
                break;
            case PathState.Visible:
                foreach (var pd in PathDots) {
                    pd.color = Config.GfxPathDotsNotSelectedAlpha;
                    pd.gameObject.SetActive(true);;
                }
                break;
            case PathState.Emphasized:
                foreach (var pd in PathDots) {
                    pd.color = Color.white;
                    pd.gameObject.SetActive(true);;
                }
                break;
            default:
                throw new Exception($"Cannot understand {_state}");
            }
        }
    }
}
