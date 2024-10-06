using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class POIView: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public bool SkipThisLocation;

    public Encounter[] Encounters;

    public int LowAnimals;
    public int LowPlants;
    public int LowMagic;
    public int LowBeauty;

    public int HighAnimals = 3;
    public int HighPlants = 3;
    public int HighMagic = 3;
    public int HighBeauty = 3;

    public string FriendlyName;
    public string KnotName;
    public string Animals;
    public string Plants;
    public string BadPlants;

    [TextArea(3, 10)]
    public string Description;

    [SerializeField] Image _image;
    [SerializeField] Button _button;

    public bool Highlighted {
        get => _image.color == Color.white;
        set {
            _image.color = value == true ? Color.white : LD.Cfg.GfxMapNotSelectedColorOverlay;
            _button.interactable = value;
        }
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
