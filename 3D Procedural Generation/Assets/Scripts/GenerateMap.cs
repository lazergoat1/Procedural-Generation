using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject parent;
    public GameObject prefab;

    public int height;
    public int width;

    public int seed;
    public float noise;

    public float scale;
    public float sizeMultiplier;

    public int octaves;
    [Range(0, 1)]
    public float persistance;
    public float lacunarity;


    public float offsetX;
    public float offsetY;

    public bool updateEverySeconds;
    public float updateDelay;
    float delay = 0;

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        delay -= Time.deltaTime;

        if(updateEverySeconds)
        {
            if(delay <= 0)
            {
                Generate();
                delay = updateDelay;
            }
        }
    }

    private void Generate()
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (scale <= 0)
        {
            scale = 0.001f;
        }

        float[,] noiseMap = Noise.GenerateNoise(width, height, seed, offsetX, offsetY, scale, octaves, persistance, lacunarity);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noise = noiseMap[x, y];

                GameObject newCube = Instantiate(prefab, new Vector3(x, noise * sizeMultiplier, y), Quaternion.identity);

                Color color = Color.Lerp(Color.black, Color.white, noise);
                newCube.gameObject.GetComponent<Renderer>().material.color = color;
                newCube.transform.parent = parent.transform;
            }
        }
    }

    private void OnValidate()
    {
        if(octaves <= 0)
        {
            octaves = 1;
        }

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
