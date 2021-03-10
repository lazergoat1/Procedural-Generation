using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GenerateTileMap : MonoBehaviour
{
    public BiomePreset[] biomes;
    public Tilemap tilemap;
    public GenerateMap map;

    [Header("Dimensions")]
    public int width;
    public int height;
    public float scale;
    public int seed;
    public float noise;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    [Header("Height Map")]
    public int heightOctaves;
    public float[,] heightMap;

    public bool updateEverySeconds = false;
    public float updateDelay;
    float delay = 0;

    void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        delay -= Time.deltaTime;

        if (updateEverySeconds)
        {
            if (delay <= 0)
            {
                GenerateMap();
                delay = updateDelay;
            }
        }
    }

    void GenerateMap()
    {
        tilemap.ClearAllTiles();

        if (scale <= 0)
        {
            scale = 0.001f;
        }

        heightMap = Noise.GenerateNoise(map.width, map.height, map.seed, map.offsetX, map.offsetY, map.scale, map.octaves, map.persistance, map.lacunarity);

        for (int x = 0; x < map.width; ++x)
        {
            for (int y = 0; y < map.height; ++y)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), GetBiome(heightMap[x, y]));
            }
        }
    }

    private TileBase GetBiome(float height)
    {

        TileBase tile = biomes[0].PickRandomTile();
        float minHeight = 0f;
        print(height);

        foreach (BiomePreset biome in biomes)
        {
            if (height >= biome.minHeight && biome.minHeight >= minHeight)
            {
                minHeight = biome.minHeight;
                tile = biome.PickRandomTile();
            }
        }
        return tile;
    }
    private void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (height < 1)
        {
            height = 1;
        }
    }

}
