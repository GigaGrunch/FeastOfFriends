using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    List<Character> characters;
    List<Requirement> requirements;
    List<Reward> rewards;
    List<Room> nextRooms;
    private GameController gameController;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {

    }

    public List<Character> Characters
    {
        get
        {
            return characters;
        }

        set
        {
            characters = value;
        }
    }

    public List<Requirement> Requirements
    {
        get
        {
            return requirements;
        }

        set
        {
            requirements = value;
        }
    }

    public List<Reward> Rewards
    {
        get
        {
            return rewards;
        }

        set
        {
            rewards = value;
        }
    }

    public List<Room> NextRooms
    {
        get
        {
            return nextRooms;
        }

        set
        {
            nextRooms = value;
        }
    }

    void OnMouseDown()
    {
        gameController.onRoomClicked(this);
    }
}