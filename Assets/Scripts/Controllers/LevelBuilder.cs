using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject floorPrefab;

    public int size = 10;
    // Start is called before the first frame update
    void Start()
    {
        TESTLevelWidth();
    }

    void TESTLevelWidth()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; i++)
            {
                GameObject newTile = Instantiate(floorPrefab);
                newTile.transform.position = new Vector3(i, 0, j);
            }
        }
    }
}
