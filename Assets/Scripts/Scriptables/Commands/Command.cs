using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum CommandType { MoveUp, MoveBack, RotateRigth, RotateLeft, Pick, Use, Finish, None}

public abstract class Command: ScriptableObject
{
    //public CommandType commandType;

    public abstract CommandType GetCommandType();
}
