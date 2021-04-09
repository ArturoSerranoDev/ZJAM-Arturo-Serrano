using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: Command
{
    public Vector2 dir;
    public override CommandType GetCommandType()
    {
        return CommandType.Move;
    }
}
