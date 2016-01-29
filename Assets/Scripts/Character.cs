using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private int strength;
    private int agility;
    private int vision;

    private bool isMale;
    private string charName;

    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
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

    public bool IsMale
    {
        get
        {
            return isMale;
        }

        set
        {
            isMale = value;
        }
    }

    public void feast(int strengthBonus, int agilityBonus, int visionBonus)
    {
        strength += strengthBonus;
        agility += agilityBonus;
        vision += visionBonus;
    }

    void OnMouseDown()
    {
        gameController.onCharacterClicked(this);
    }
}
