using UnityEngine;
using System.Collections;

[System.Serializable]
public class Reward
{
    public enum Type
    {
        altar, human
    }

    public Type type;
    public int visionRequired;
    private bool isActive = true;

    public bool IsActive
    {
        get
        {
            return isActive;
        }

        set
        {
            isActive = value;
        }
    }

    public Type getType()
    {
        return type;
    }

    public void setType(Type type)
    {
        this.type = type;
    }
}
