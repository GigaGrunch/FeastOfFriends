using UnityEngine;
using System.Collections;

public class Reward : Spotable
{
    enum Type
    {
        altar, human
    }

    Type type;

    Type getType()
    {
        return type;
    }
}
