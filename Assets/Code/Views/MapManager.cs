using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager: MonoBehaviour {
    [SerializeField] POIView _startPOI;
    [SerializeField] GameObject _colonyIcon;

    PathView[] _paths;
    POIView[] _pois;
    PathView _currentPath;
    POIView _currentTarget;

    List<Image> _pointsToWalkThrough;
    float _startMoveTime;

    public POIView CurrentPOI { get; private set; }

    public void DeemphasizePaths() {
        foreach (var path in _paths) {
            if (path.State == PathView.PathState.Emphasized) {
                path.State = PathView.PathState.Visible;
            }
        }
    }

    public void EmphasizePathToMe(POIView target) {
        if (target == CurrentPOI) {
            return;
        }

        var paths = GetPathsFrom(target);
        foreach (var path in paths) {
            if (path.Start == CurrentPOI || path.End == CurrentPOI) {
                path.State = PathView.PathState.Emphasized;
            }
        }
    }

    public PathView[] GetPathsFrom(POIView origin) {
        var rv = new List<PathView>();
        foreach (var path in _paths) {
            if (CurrentPOI != _startPOI && path.Start == _startPOI) {
                continue;
            }
            if (path.Start == origin || path.End == origin) {
                rv.Add(path);
            }
        }
        return rv.ToArray();
    }

    public void HidePaths() {
        foreach (var path in _paths) {
            path.State = PathView.PathState.Hidden;
        }
        foreach (var poi in _pois) {
            poi.Highlighted = false;
        }
    }

    public void OnPOIClicked(PathView path, POIView poi) {
        foreach (var pv in _paths) {
            pv.State = pv == path ? PathView.PathState.Emphasized : PathView.PathState.Hidden;
        }
        _currentPath = path;
        _currentTarget = poi;

        _pointsToWalkThrough.Clear();
        _pointsToWalkThrough.AddRange(path.PathDots);
        if (poi == path.Start) {
            _pointsToWalkThrough.Reverse();
        }

        _startMoveTime = Time.time;
        HidePaths();
        _currentPath.State = PathView.PathState.Emphasized;
        _currentTarget.Highlighted = true;
        _pointsToWalkThrough[0].gameObject.SetActive(false);
        if (_pointsToWalkThrough.Count > 0) {
            _pointsToWalkThrough[1].gameObject.SetActive(false);
        }

        Game.SetState(GameData.GameState.Traveling);
    }

    public void OnStartNewGame() {
        _currentPath = null;
        _currentTarget = null;
        _pointsToWalkThrough = new List<Image>();
        CurrentPOI = _startPOI;
        _colonyIcon.transform.position = _startPOI.transform.position;
        HidePaths();
    }

    public void ShowValidPaths() {
        HidePaths();
        foreach (var path in GetPathsFrom(CurrentPOI)) {
            path.State = PathView.PathState.Visible;
            path.Start.Highlighted = true;
            path.End.Highlighted = true;
        }
        CurrentPOI.Highlighted = false;
    }

    // Private

    void arriveAtDestination() {
        _colonyIcon.transform.position = _currentTarget.transform.position;
        CurrentPOI = _currentTarget;
        _currentTarget = null;
        _currentPath = null;
        _startMoveTime = 0;

        Game.SetState(GameData.GameState.Encounter);
    }

    void Awake() {
        _paths = GameObject.FindObjectsByType<PathView>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        _pois = GameObject.FindObjectsByType<POIView>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    void Start() {
        foreach (var path in _paths) {
            path.gameObject.SetActive(true);
        }
    }

    void Update() {
        if (_startMoveTime == 0) {
            return;
        }

        var speed = LD.Cfg.GfxPathTravelSpeed;

        while (Time.time > _startMoveTime + speed) {
            _pointsToWalkThrough[0].gameObject.SetActive(false);
            if (_pointsToWalkThrough.Count > 1) {
                _pointsToWalkThrough[1].gameObject.SetActive(false);
            }
            _pointsToWalkThrough.RemoveAt(0);
            if (_pointsToWalkThrough.Count == 0) {
                arriveAtDestination();
                return;
            }
            _startMoveTime += speed;
        }

        var from = _pointsToWalkThrough[0];
        var progress = Time.time - _startMoveTime;
        var to = (_pointsToWalkThrough.Count > 1)
            ? _pointsToWalkThrough[1].transform.position
            : _currentTarget.transform.position;

        _colonyIcon.transform.position = Vector3.Lerp(from.transform.position, to, progress / speed);
    }
}
