using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Command/Move", fileName = "Move", order = 1)]

public class Move: Command
{
    // 1 forward, -1 backwards
    public int dir;
    public override CommandType GetCommandType()
    {
        return CommandType.Move;
    }
}
