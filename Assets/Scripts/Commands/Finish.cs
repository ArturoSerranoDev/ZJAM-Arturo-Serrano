using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish: Command
{
    public override CommandType GetCommandType()
    {
        return CommandType.Finish;
    }
}

