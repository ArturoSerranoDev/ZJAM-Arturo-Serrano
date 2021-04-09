using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject floorPrefab;
    public GameObject playerPrefab;

    public GameObject player;

    public List<GameObject> tilesInLevel = new List<GameObject>();
    public List<GameObject> enemiesInLevel = new List<GameObject>();

    public int size = 10;
    // Start is called before the first frame update
    void Start()
    {
        //TESTLevelWidth();
    }

    void TESTLevelWidth()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GameObject newTile = Instantiate(floorPrefab);
                newTile.transform.position = new Vector3(i - 4, -1, j - 4);
            }
        }
    }

    public void BuildLevel(LevelData levelData)
    {
        SpawnTiles(levelData);

        SpawnBuildings(levelData);

        SpawnEnemies(levelData);

        SpawnPlayer(levelData);

        Debug.Log("Level Built");
    }

    void SpawnPlayer(LevelData levelData)
    {
        player = PoolManager.Instance.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);

        Debug.Log("SpawnPlayer");

    }

    void SpawnEnemies(LevelData levelData)
    {
        Debug.Log("SpawnEnemies");
    }

    void SpawnBuildings(LevelData levelData)
    {
        Debug.Log("SpawnBuildings");
    }

    void SpawnTiles(LevelData levelData)
    {

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                string tileLetter = levelData.levelTiles[j + 10 * i];

                //Spawn depending on the tileLetter
                GameObject spawnedTile;
                switch (tileLetter)
                {
                    case "T":
                        spawnedTile = PoolManager.Instance.Spawn(floorPrefab, Vector3.zero, Quaternion.identity);
                        spawnedTile.transform.position = new Vector3(i - 4, 0, j - 4);
                        tilesInLevel.Add(spawnedTile);

                        break;

                    default:
                        break;
                }


            }
        }

        Debug.Log("SpawnTiles");

    }

    public IEnumerator LoadLevelCoroutine()
    {
        // yield all tiles spawn
        //TODO: SPAWN FROM CENTER IN FLOWER PATTERN
        foreach (GameObject tile in tilesInLevel)
        {
            yield return StartCoroutine(tile.GetComponent<TweenAction>().ExecuteTween());
        }

        // yield all buildings spawn

        // yield all enemies spawn


        yield return new WaitForEndOfFrame();
    }
}
