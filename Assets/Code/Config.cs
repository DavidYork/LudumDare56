using UnityEngine;

public class Config: MonoBehaviour {
    public Color GfxMapNotSelectedColorOverlay = new Color(.8f, .8f, .8f, 1f);

    [Tooltip("Time spent to get from dot to dot")]
    public float GfxPathTravelSpeed = .2f;
}
