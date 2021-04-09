using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType { Move, Rotate, Pick, Use, Finish}

public abstract class Command: ScriptableObject
{
    //public CommandType commandType;

    public abstract CommandType GetCommandType();
}
