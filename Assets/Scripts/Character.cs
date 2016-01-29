using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private int strength;
    private int agility;
    private int vision;

    private string charName;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public int Agility
    {
        get
        {
            return agility;
        }

        set
        {
            agility = value;
        }
    }

    public int Vision
    {
        get
        {
            return vision;
        }

        set
        {
            vision = value;
        }
    }

    public string CharName
    {
        get
        {
            return charName;
        }

        set
        {
            charName = value;
        }
    }
}
