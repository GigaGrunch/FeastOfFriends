using UnityEngine;
using System.Collections;

public class Reward : MonoBehaviour
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
