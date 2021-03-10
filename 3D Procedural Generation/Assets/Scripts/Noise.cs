using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static float[,] GenerateNoise(int width, int height, int seed, float offsetX, float offsetY, float scale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[width, height];

        System.Random randomSeed = new System.Random(seed);

        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float octaveOffsetX = randomSeed.Next(-100000, 100000) + offsetX;
            float octaveOffsetY = randomSeed.Next(-100000, 100000) + offsetY;

            octaveOffsets[i] = new Vector2(octaveOffsetX, octaveOffsetY);
        }

        if (scale <= 0)
        {
            scale = 0.001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float frequency = 1f;
                float amplitude = 1f;
                float noiseHeight = 0f;

                for (int i = 0; i < octaves; i++)
                {
                    float xCoord = (float)x / scale * frequency + octaveOffsets[i].x;
                    float yCoord = (float)y / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(xCoord, yCoord) * 2 -1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }

}
