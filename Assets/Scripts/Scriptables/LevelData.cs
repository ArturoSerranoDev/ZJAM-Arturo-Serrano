using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectiveType {Survive, Escape, Kill}
public enum EnemyLoopType {Additive, Reverse}

[CreateAssetMenu(menuName = "Create LevelData", fileName = "LevelData", order = 0)]
public class LevelData : ScriptableObject
{
   public int levelNumber;
   public Vector3 playerStartingPos;
   public List<EnemyData> enemiesData;
   public List<CommandData> commandData;

    public DialogData levelDialog;

   public List<string> levelTiles;
   public ObjectiveType levelObjective;
   public int turnLimit = 100;
}

[System.Serializable]
public class EnemyData
{
   public GameObject enemyPrefab;
   public Vector2 enemyPos;
   public List<Command> enemyCommands;
   public EnemyLoopType commandLoopType;
}

[System.Serializable]
public class CommandData
{
    public CommandType commandType;
    public int amount;
}