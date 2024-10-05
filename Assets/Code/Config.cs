using UnityEngine;

public class Config: MonoBehaviour {
    public Color GfxMapNotSelectedColorOverlay = new Color(.8f, .8f, .8f, 1f);

    [Tooltip("Time spent to get from dot to dot")]
    public float GfxPathTravelSpeed = .2f;

    public Color GfxResourceGoodColor = new Color(.7f, 1f, .8f, 1f);
    public Color GfxResourceBadColor = new Color(1f, .7f, .7f, 1f);
    public float GfxResourceWiggleDuration = 1f;
}
