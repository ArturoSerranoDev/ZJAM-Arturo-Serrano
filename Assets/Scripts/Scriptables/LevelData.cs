using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType {Survive, Escape, Kill}

[CreateAssetMenu(menuName = "Create LevelData", fileName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
   public int levelNumber;
   public Vector2 playerStartingPos;
   public List<EnemyData> enemiesData;

   public List<string> levelTiles;
   public ObjectiveType levelObjective;
}

[System.Serializable]
public class EnemyData
{
   public GameObject enemyPrefab;
   public Vector2 enemyPos;
   public List<Vector2> enemyPatrolPoints;
}