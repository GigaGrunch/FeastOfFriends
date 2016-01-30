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