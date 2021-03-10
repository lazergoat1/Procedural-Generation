using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPositioner : MonoBehaviour
{
    public GenerateMap map;

    void Update()
    {
        transform.position = new Vector3(map.width / 2, map.width + map.height/2, map.height / 2); 
    }
}
