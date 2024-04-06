using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoardSettings", menuName = "ScriptableObjects/BoardSettings", order = 51)]
public class BoardSettings : ScriptableObject
{
    public GameObject tilePrefab;
    public Vector2 firstTilePositionOffset;
    public float tileOffset;
    public int rows;
    public int columns;
}