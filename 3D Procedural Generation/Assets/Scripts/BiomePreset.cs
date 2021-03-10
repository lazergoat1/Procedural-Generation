using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Biome Preset", menuName = "New Biome Preset")]
public class BiomePreset : ScriptableObject
{
    public TileBase[] tiles;
    public float minHeight;
    public float minMoisture;
    public float minHeat;

    public TileBase PickRandomTile()
    {
        return tiles[Random.Range(0, tiles.Length)];
    }
}
