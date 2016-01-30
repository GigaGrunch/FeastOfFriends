using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    List<Character> characters;
    List<Requirement> requirements;
    List<Reward> rewards;
    private GameController gameController;
    List<Character> selectedCharacters = new List<Character>();
    List<Movement> pendingMovements = new List<Movement>();

    [SerializeField]
    Room northRoom;
    [SerializeField]
    Room eastRoom;
    [SerializeField]
    Room westRoom;
    [SerializeField]
    Room southRoom;

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

    public Room NorthRoom
    {
        get
        {
            return northRoom;
        }

        set
        {
            northRoom = value;
        }
    }

    public Room EastRoom
    {
        get
        {
            return eastRoom;
        }

        set
        {
            eastRoom = value;
        }
    }

    public Room WestRoom
    {
        get
        {
            return westRoom;
        }

        set
        {
            westRoom = value;
        }
    }

    public Room SouthRoom
    {
        get
        {
            return southRoom;
        }

        set
        {
            southRoom = value;
        }
    }

    public List<Character> SelectedCharacters
    {
        get
        {
            return selectedCharacters;
        }

        set
        {
            selectedCharacters = value;
        }
    }

    // left mouse button
    void OnMouseDown()
    {
        gameController.onRoomClicked(this);
    }

    void OnMouseOver()
    {
        // right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Room selectedRoom = gameController.SelectedRoom;
            // can only move to neighbouring rooms
            if (selectedRoom.NorthRoom == this || selectedRoom.EastRoom == this || selectedRoom.WestRoom == this || selectedRoom.SouthRoom == this)
            {
                foreach(Character movingChar in selectedRoom.SelectedCharacters)
                {
                    pendingMovements.Add(new Movement(selectedRoom, this, movingChar));
                }
            }
        }
    }

    public void resolvePendingMovements()
    {
        foreach (Movement pendingMovement in pendingMovements)
        {
            // test requirements here
            bool success = true;
            if (success)
            {
                Character character = pendingMovement.Character;
                pendingMovement.Source.Characters.Remove(character);
                pendingMovement.Destination.Characters.Add(character);
            }
        }
    }
}