using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Command/Move", fileName = "Move", order = 1)]

public class Move: Command
{
    public Vector2 dir;
    public override CommandType GetCommandType()
    {
        return CommandType.Move;
    }
}
