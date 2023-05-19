using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerControlTypeHolder
{
    public enum ControlTypeEnum
    {
        Both,
        Mouse,
        WASD
    }

    private static ControlTypeEnum controlType; 

    public static ControlTypeEnum ControlType
    {
        get { return controlType; }
        set { controlType = value; }
    }
}