using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Command/Finish", fileName = "Finish", order = 1)]

public class Finish: Command
{
    public override CommandType GetCommandType()
    {
        return CommandType.Finish;
    }
}

