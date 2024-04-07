using UnityEngine;

[CreateAssetMenu(fileName = "TileSettings", menuName = "ScriptableObjects/TileSettings", order = 52)]
public class TileSettings : ScriptableObject
{
    [Header("Color Settings")]
    public Color defaultColor;
    public Color playerColor;
    public Color enemyColor;

    [Header("Text Settings")]
    public string defaultText;
    public string playerText;
    public string enemyText;

    [Header("Animation Settings")]
    public float fadeDuration;
    public float finalAlpha;
}
