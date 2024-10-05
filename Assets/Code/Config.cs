using UnityEngine;

public static class Config {
    public static Color GfxPathDotsNotSelectedAlpha = new Color(.8f, .8f, .8f, 1f);

    [Tooltip("Time spent to get from dot to dot")]
    public static float GfxPathTravelSpeed = .2f;
}
