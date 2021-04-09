using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Use: Command
{
    public override CommandType GetCommandType()
    {
        return CommandType.Use;
    }
}