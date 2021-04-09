using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Command/Use", fileName = "Use", order = 1)]
public class Use: Command
{
    public override CommandType GetCommandType()
    {
        return CommandType.Use;
    }
}