using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject floorPrefab;
    public GameObject evacPrefab;
    public GameObject playerPrefab;

    public GameObject player;

    public List<GameObject> tilesInLevel = new List<GameObject>();
    public List<EnemyView> enemiesInLevel = new List<EnemyView>();
    public List<BuildingView> buildings = new List<BuildingView>();

    public Material floorMat;


    public int size = 10;
    public float turnSpeed = 0.5f;
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
                newTile.transform.position = new Vector3(i, 0, j);
            }
        }
    }

    public void DespawnAll()
    {
        PoolManager.Instance.Despawn(player.gameObject);

        foreach (EnemyView item in enemiesInLevel)
        {
            PoolManager.Instance.Despawn(item.gameObject);
        }

        foreach (BuildingView item in buildings)
        {
            PoolManager.Instance.Despawn(item.gameObject);
        }

    }

    public void BuildLevel(LevelData levelData)
    {
        floorMat.SetTexture("_BaseMap", levelData.floorMat);

        SpawnTiles(levelData);

        SpawnBuildings(levelData);

        SpawnEnemies(levelData);

        SpawnPlayer(levelData);

        Debug.Log("Level Built");
    }

    void SpawnPlayer(LevelData levelData)
    {
        player = PoolManager.Instance.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);

        player.GetComponent<PlayerController>().Reset();

        player.GetComponent<PlayerController>().animSpeed = turnSpeed;

        player.transform.position = levelData.playerStartingPos;

        player.gameObject.SetActive(false);

        Debug.Log("SpawnPlayer");

    }

    void SpawnEnemies(LevelData levelData)
    {
        foreach (EnemyData enemyData in levelData.enemiesData)
        {
            GameObject newEnemy = PoolManager.Instance.Spawn(enemyData.enemyPrefab, Vector3.zero, Quaternion.identity);

            newEnemy.transform.position = new Vector3(enemyData.enemyPos.x,0, enemyData.enemyPos.y);
            EnemyView enemyView = newEnemy.GetComponent<EnemyView>();

            enemyView.Reset(enemyData);
            enemyView.animSpeed = turnSpeed;

            enemyView.enemyLoopType = enemyData.commandLoopType;
            enemyView.enemyCommands = enemyData.enemyCommands;

            enemiesInLevel.Add(enemyView);

        }

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
                        spawnedTile = PoolManager.Instance.Spawn(levelData.tilePrefab, Vector3.zero, Quaternion.identity);
                        spawnedTile.transform.position = new Vector3(i, 0, j);
                        tilesInLevel.Add(spawnedTile);
                        spawnedTile.transform.GetChild(0).localScale = Vector3.zero;

                        break;
                    case "E":
                        spawnedTile = PoolManager.Instance.Spawn(evacPrefab, Vector3.zero, Quaternion.identity);
                        spawnedTile.transform.position = new Vector3(i, 0, j);
                        tilesInLevel.Add(spawnedTile);
                        spawnedTile.transform.GetChild(0).localScale = Vector3.zero;

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

        player.gameObject.SetActive(true);

        // yield all buildings spawn

        // yield all enemies spawn


        yield return new WaitForEndOfFrame();
    }
}
