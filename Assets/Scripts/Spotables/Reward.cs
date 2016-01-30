using UnityEngine;
using System.Collections;

public class Reward : Spotable
{
    public enum Type
    {
        altar, human
    }

    Type type;

    public Type getType()
    {
        return type;
    }

    public void setType(Type type)
    {
        this.type = type;
    }
}
