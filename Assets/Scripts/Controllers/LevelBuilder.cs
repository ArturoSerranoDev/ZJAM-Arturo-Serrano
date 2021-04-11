using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LevelBuilder : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject floorPrefab;
    public GameObject evacPrefab;
    public GameObject playerPrefab;

    public GameObject player;
    public GameObject evac;

    public GameObject levelBuildingsParent;
    public List<GameObject> tilesInLevel = new List<GameObject>();
    public List<EnemyView> enemiesInLevel = new List<EnemyView>();
    public List<GameObject> buildings = new List<GameObject>();

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

        foreach (GameObject item in buildings)
        {
            PoolManager.Instance.Despawn(item.gameObject);
        }
        levelBuildingsParent.transform.GetChild(LevelController.Instance.currentLevel - 1).gameObject.SetActive(false);

        foreach (GameObject item in tilesInLevel)
        {
            PoolManager.Instance.Despawn(item.gameObject);
        }

        PoolManager.Instance.Despawn(evac.gameObject);

        enemiesInLevel.Clear();
        buildings.Clear();
        tilesInLevel.Clear();

    }

    public void BuildLevel(LevelData levelData)
    {
        floorMat.SetTexture("_BaseMap", levelData.floorMat);

        SpawnTiles(levelData);


        SpawnEnemies(levelData);

        SpawnPlayer(levelData);

        evac = PoolManager.Instance.Spawn(evacPrefab, Vector3.zero, Quaternion.identity);
        evac.transform.position = levelData.escapePodPos;

        Debug.Log("Level Built");
    }

    void SpawnPlayer(LevelData levelData)
    {
        player = PoolManager.Instance.Spawn(playerPrefab, Vector3.zero, Quaternion.identity);

        player.GetComponent<PlayerController>().Reset();

        player.GetComponent<PlayerController>().animSpeed = turnSpeed;

        player.transform.position = levelData.playerStartingPos;


        Debug.Log("SpawnPlayer");

    }

    void SpawnEnemies(LevelData levelData)
    {
        foreach (EnemyData enemyData in levelData.enemiesData)
        {
            GameObject newEnemy = PoolManager.Instance.Spawn(enemyData.enemyPrefab, Vector3.zero, Quaternion.identity);

            newEnemy.transform.position = enemyData.enemyPos;
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
        Transform BuildingsParent = levelBuildingsParent.transform.GetChild(levelData.levelNumber - 1);

        foreach (Transform item in BuildingsParent.transform)
        {
            item.gameObject.SetActive(true);
            item.transform.DOScale(1, 0.5f).From(0);
            buildings.Add(item.gameObject);
        }
    }

    void SpawnTiles(LevelData levelData)
    {

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                string tileLetter = levelData.levelTiles[j + 10 * i];

                GameObject spawnedTile;
                spawnedTile = PoolManager.Instance.Spawn(levelData.tilePrefab, Vector3.zero, Quaternion.identity);
                spawnedTile.transform.position = new Vector3(i, 0, j);
                tilesInLevel.Add(spawnedTile);
                spawnedTile.transform.GetChild(0).localScale = Vector3.zero;

                ////Spawn depending on the tileLetter
                //switch (tileLetter)
                //{
                //    case "T":
                //        spawnedTile = PoolManager.Instance.Spawn(levelData.tilePrefab, Vector3.zero, Quaternion.identity);
                //        spawnedTile.transform.position = new Vector3(i, 0, j);
                //        tilesInLevel.Add(spawnedTile);
                //        spawnedTile.transform.GetChild(0).localScale = Vector3.zero;

                //        break;
                //    case "E":
                //        spawnedTile = PoolManager.Instance.Spawn(evacPrefab, Vector3.zero, Quaternion.identity);
                //        spawnedTile.transform.position = new Vector3(i, 0, j);
                //        tilesInLevel.Add(spawnedTile);
                //        spawnedTile.transform.GetChild(0).localScale = Vector3.zero;

                //        break;

                //    default:
                //        break;
                //}


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
            StartCoroutine(tile.GetComponent<TweenAction>().ExecuteTween());
            yield return new WaitForEndOfFrame();
        }

        SpawnBuildings(LevelController.Instance.levelData);

        yield return StartCoroutine(player.GetComponent<TweenAction>().ExecuteTween());


        // yield all buildings spawn
        foreach (GameObject tile in buildings)
        {
            //yield return StartCoroutine(tile.GetComponent<TweenAction>().ExecuteTween());
        }
        // yield all enemies spawn
        foreach (EnemyView enemy in enemiesInLevel)
        {
            yield return StartCoroutine(enemy.GetComponent<TweenAction>().ExecuteTween());
        }

        yield return new WaitForEndOfFrame();
    }
}
