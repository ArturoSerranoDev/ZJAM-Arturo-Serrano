using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Command/Rotate", fileName = "Rotate", order = 1)]
public class Rotate: Command
{
    // 1 moves right, -1 moves left
    public int rotDir;
    public override CommandType GetCommandType()
    {
        return rotDir == 1 ? CommandType.RotateRigth : CommandType.RotateLeft;
    }
}