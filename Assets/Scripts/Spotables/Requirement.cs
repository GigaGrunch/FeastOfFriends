using UnityEngine;
using System.Collections;

public class Requirement : Spotable
{
    public enum Type
    {
        strength, agility
    }

    Type type;
    int requiredValue;
    int visionRequired;

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
