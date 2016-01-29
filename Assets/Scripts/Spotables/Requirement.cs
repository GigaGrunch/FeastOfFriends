using UnityEngine;
using System.Collections;

public class Requirement : Spotable
{
    enum Type
    {
        strentgh, speed
    }

    Type type;
    int value;

    Type getType()
    {
        return type;
    }

    int getValue()
    {
        return value;
    }
}
