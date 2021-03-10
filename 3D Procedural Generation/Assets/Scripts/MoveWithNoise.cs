using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithNoise : MonoBehaviour
{
    //public Vector2 perlinPosition;
    public float noise;
    public float multiplier;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float elapsedTime = Time.time;
        noise = Mathf.PerlinNoise(elapsedTime, 0);
        noise = Mathf.Clamp(noise, 0, 1);

        transform.position = new Vector3(noise * multiplier,0, 0);
    }
}
