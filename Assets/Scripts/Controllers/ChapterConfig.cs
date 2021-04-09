using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ChapterConfig", fileName = "LevelData", order = 0)]
public class ChapterConfig : ScriptableObject
{
    public List<LevelData> levels;
}