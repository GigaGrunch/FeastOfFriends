using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Room : MonoBehaviour
{
    List<Character> characters = new List<Character>();
    List<Requirement> requirements;
    List<Reward> rewards;
    private GameController gameController;
    List<Character> selectedCharacters = new List<Character>();
    List<Movement> pendingMovementsNorth = new List<Movement>();
    List<Movement> pendingMovementsEast = new List<Movement>();
    List<Movement> pendingMovementsSouth = new List<Movement>();
    List<Movement> pendingMovementsWest = new List<Movement>();

    [SerializeField]
    GameObject WallTop, WallBottom, WallLeft, WallRight;
    [SerializeField]
    GameObject CornerTopLeft, CornerTopRight, CornerBottomLeft, CornerBottomRight;
    [SerializeField]
    GameObject DoorLeft, DoorRight, DoorTop, DoorBottom;
    [SerializeField]
    GameObject Tile;
    [SerializeField]
    GameObject TunnelHor, TunnelVer;

    [SerializeField]
    Room northRoom;
    [SerializeField]
    Room eastRoom;
    [SerializeField]
    Room westRoom;
    [SerializeField]
    Room southRoom;
    [SerializeField]
    int BonusFactor = 4;

    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GameObject temp;

        gameController = FindObjectOfType<GameController>();

        temp = Instantiate(CornerBottomLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, -.75f, 0);

        temp = Instantiate(CornerBottomRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.75f, 0);

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);

        temp = Instantiate(CornerTopRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, .75f, 0);

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.25f, 0);

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, -.25f, 0);

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .25f, 0);

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, .25f, 0);

        temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.75f, 0);

        temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .25f, 0);

        temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .75f, 0);

        temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.25f, 0);

        if (northRoom != null)
        {
            temp = Instantiate(DoorTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.25f, 0);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.75f, 0);
        }
        else
        {
            temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);
        }

        if (southRoom != null)
        {
            temp = Instantiate(DoorBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.25f, 0);

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.75f, 0);
        }
        else
        {
            temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);
        }

        if (eastRoom != null)
        {
            temp = Instantiate(DoorRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.25f, .25f, 0);

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.75f, .25f, 0);
        }
        else
        {
            temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);
        }

        if (westRoom != null)
        {
            temp = Instantiate(DoorLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.25f, -.25f, 0);

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.75f, -.25f, 0);
        }
        else
        {
            temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);
        }
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
        if(gameController.SelectedRoom != this)
        {
            gameController.onRoomSelected(this);
        }
        
    }

    void OnMouseOver()
    {
        // right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            Room selectedRoom = gameController.SelectedRoom;
            // can only move to neighbouring rooms
            if (selectedRoom.NorthRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    pendingMovementsNorth.Add(new Movement(selectedRoom, this, movingChar));
                }
            }
            else if (selectedRoom.EastRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    pendingMovementsEast.Add(new Movement(selectedRoom, this, movingChar));
                }
            }
            else if (selectedRoom.WestRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    pendingMovementsSouth.Add(new Movement(selectedRoom, this, movingChar));
                }
            }
            else if (selectedRoom.SouthRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    pendingMovementsWest.Add(new Movement(selectedRoom, this, movingChar));
                }
            }
        }
    }

    public void resolvePendingMovements()
    {
        if (pendingMovementsEast.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsEast);
        }
        if (pendingMovementsSouth.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsSouth);
        }
        if (pendingMovementsWest.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsWest);
        }
        if (pendingMovementsNorth.Count > 0)
        {
            tryToExecuteMovement(pendingMovementsNorth);
        }

    }

    private void tryToExecuteMovement(List<Movement> pendingMovements)
    {
        // test requirements here
        List<Requirement> destinationRequirements = pendingMovements[0].Destination.Requirements;
        bool success = false;
        foreach (Movement pendingMovement in pendingMovements)
        {
            if (destinationRequirements.Count == 0)
            {
                success = true;
                break;
            }
            Character character = pendingMovement.Character;
            foreach (Requirement r in destinationRequirements)
            {
                if (r.getType() == Requirement.Type.agility)
                {
                    if (character.Agility >= r.getRequiredValue())
                    {
                        destinationRequirements.Remove(r);
                    }
                }
                else if (r.getType() == Requirement.Type.strength)
                {
                    if (character.Strength >= r.getRequiredValue())
                    {
                        destinationRequirements.Remove(r);
                    }
                }
            }
        }        

        if (success)
        {
            foreach (Movement pendingMovement in pendingMovements)
            {
                gameController.OnPlayerMovementSuccess(pendingMovement.Source, pendingMovement.Destination, pendingMovement.Character);
            }                
        }
        pendingMovements.Clear();
    }

    public void sacrifice()
    {
        Character victim = selectedCharacters[selectedCharacters.Count - 1];
        selectedCharacters.Remove(victim);
        characters.Remove(victim);
        gameController.killCharacter(victim);
        foreach(Character c in characters)
        {
            c.feast(victim.Strength / 4, victim.Agility / 4, victim.Vision / 4);
        }
    }


}