using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class POIView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] Image _image;

    public bool Highlighted {
        get => _image.color == Color.white;
        set => _image.color = value == true ? Color.white : LD.Cfg.GfxMapNotSelectedColorOverlay;
    }

    public void OnClick() {
        if (!Highlighted || LD.Data.State != GameData.GameState.ChoosingDestination) {
            return;
        }

        foreach (var path in UI.MapMgr.GetPathsFrom(this)) {
            if (path.Start == UI.MapMgr.CurrentPOI || path.End == UI.MapMgr.CurrentPOI) {
                UI.MapMgr.OnPOIClicked(path, this);
                return;
            }
        }

        Debug.LogError($"Cannot find valid path from {UI.MapMgr.CurrentPOI?.name ?? "<null>"} to {name}");
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (!Highlighted || LD.Data.State != GameData.GameState.ChoosingDestination) {
            return;
        }

        UI.MapMgr.EmphasizePathToMe(this);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!Highlighted || LD.Data.State != GameData.GameState.ChoosingDestination) {
            return;
        }

        UI.MapMgr.DeemphasizePaths();
    }
}
