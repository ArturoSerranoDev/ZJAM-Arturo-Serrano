using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate: Command
{
    // 1 moves right, -1 moves left
    public int rotDir;
    public override CommandType GetCommandType()
    {
        return CommandType.Rotate;
    }
}