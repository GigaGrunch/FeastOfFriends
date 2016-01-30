using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Requirement
{
    public enum Type
    {
        strength, agility
    }

    public Type type;
    public int requiredValue;
    public int visionRequired;

    public Type getType()
    {
        return type;
    }

    public int getRequiredValue()
    {
        return requiredValue;
    }

    public int VisionRequired
    {
        get
        {
            return visionRequired;
        }

        set
        {
            visionRequired = value;
        }
    }
}
