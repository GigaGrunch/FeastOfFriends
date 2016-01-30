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

    public Type getType()
    {
        return type;
    }

    public void setType(Type type)
    {
        this.type = type;
    }
}
