﻿using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    private int strength;
    private int agility;
    private int vision;

    private bool isMale;
    private string charName;

    private Sprite portrait;

    private GameController gameController;

    bool isCurrentlyMoving;

    // Use this for initialization
    void Start()
    {
        gameController = FindObjectOfType<GameController>();

        agility = 5;
        strength = 5;
        vision = 5;
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

    public Sprite Portrait
    {
        get
        {
            return portrait;
        }

        set
        {
            portrait = value;
        }
    }

    public bool IsCurrentlyMoving
    {
        get
        {
            return isCurrentlyMoving;
        }

        set
        {
            isCurrentlyMoving = value;
        }
    }

    public void feast(int strengthBonus, int agilityBonus, int visionBonus)
    {
        strength += strengthBonus;
        agility += agilityBonus;
        vision += visionBonus;
    }

    void OnCharacterClicked()
    {
        gameController.onCharacterClicked(this);
    }
}
