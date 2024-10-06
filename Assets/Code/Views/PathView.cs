using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathView: MonoBehaviour {
    public enum PathState { Hidden, Visible, Emphasized }

    public POIView Start;
    public POIView End;

    PathState _state;

    public Image[] _pathDots { get; private set; }

    public PathState State {
        get => _state;
        set {
            _state = value;
            switch (_state) {
            case PathState.Hidden:
                foreach (var pd in _pathDots) {
                    pd.gameObject.SetActive(false);
                }
                break;
            case PathState.Visible:
                foreach (var pd in _pathDots) {
                    pd.color = LD.Cfg.GfxMapNotSelectedColorOverlay;
                    pd.gameObject.SetActive(true);
                }
                break;
            case PathState.Emphasized:
                foreach (var pd in _pathDots) {
                    pd.color = Color.white;
                    pd.gameObject.SetActive(true);
                }
                break;
            default:
                throw new Exception($"Cannot understand {_state}");
            }
        }
    }

    // Private

    void Awake() {
        var images = new List<Image>();
        foreach (Transform child in transform) {
            var img = child.GetComponent<Image>();
            if (img == null) {
                continue;
            }
            images.Add(img);
        }
        images.Sort(compare);
        _pathDots = images.ToArray();

        var splits = name.Split(" -> ");

        var from = GameObject.Find(splits[0])?.GetComponent<POIView>();
        if (from == null) {
            Debug.LogError($"Cannot find start: {from} in {name}");
        } else {
            Start = from;
        }

        var to = GameObject.Find(splits[1])?.GetComponent<POIView>();
        if (to == null) {
            Debug.LogError($"Cannot find end: {from} in {name}");
        } else {
            End = to;
        }
    }

    int compare(Image l, Image r) => string.Compare(l.name, r.name);
}
