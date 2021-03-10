using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayTexture : MonoBehaviour
{
    public RawImage image;
    public GenerateMap map;

    public int height;
    public int width;

    public float noise;
    float scale;

    float offsetX;
    float offsetY;

    private void Update()
    {
        scale = map.scale;
        offsetX = map.offsetX;
        offsetY = map.offsetY;
        width = map.width;
        height = map.height;

        float[,] noiseMap = Noise.GenerateNoise(width, height, map.seed, offsetX, offsetY, scale, map.octaves, map.persistance, map.lacunarity);

        image.texture = GenerateTexture(noiseMap);
    }

    private Texture2D GenerateTexture(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
}
