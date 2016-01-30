﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Room : MonoBehaviour
{
    public Reward[] reward;
    public Requirement[] requirement;

    List<Character> characters = new List<Character>();
    List<Requirement> requirements = new List<Requirement>();
    private GameController gameController;
    List<Character> selectedCharacters = new List<Character>();
    List<Movement> pendingMovementsNorth = new List<Movement>();
    List<Movement> pendingMovementsEast = new List<Movement>();
    List<Movement> pendingMovementsSouth = new List<Movement>();
    List<Movement> pendingMovementsWest = new List<Movement>();

    List<GameObject> fades = new List<GameObject>();

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
    GameObject Fade;

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

    [SerializeField]
    GameObject blackSmog;

    [SerializeField]
    GameObject selectBubble;

    public void Initialize()
    {
        GetComponent<SpriteRenderer>().enabled = false;

        transform.Translate(transform.position.y / 4 * -.5f, transform.position.x / 4 * .5f, 0);

        GameObject temp;

        gameController = FindObjectOfType<GameController>();

        temp = Instantiate(CornerBottomLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerBottomRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(CornerTopLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(Tile, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.25f, -.75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(-.75f, .25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.25f, .75f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
        temp.transform.parent = transform;
        temp.transform.Translate(.75f, -.25f, 0);
        temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

        if (northRoom != null)
        {
            temp = Instantiate(DoorTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-.25f, 1.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-.25f, 1.75f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallTop, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, .75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if (southRoom != null)
        {
            temp = Instantiate(DoorBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(.25f, -1.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(.25f, -1.75f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallBottom, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if (eastRoom != null)
        {
            temp = Instantiate(DoorRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(1.25f, .25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(1.75f, .25f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallRight, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }

        if (westRoom != null)
        {
            temp = Instantiate(DoorLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-1.25f, -.25f, 0);
            //tunnels.Add(temp);

            //temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            //temp.transform.parent = transform;
            //temp.transform.Translate(-1.75f, -.25f, 0);
            //tunnels.Add(temp);
        }
        else
        {
            temp = Instantiate(WallLeft, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
        }
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

    public Reward[] Reward
    {
        get
        {
            return reward;
        }

        set
        {
            reward = value;
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

    public List<GameObject> Fades
    {
        get
        {
            return fades;
        }

        set
        {
            fades = value;
        }
    }

    public GameObject BlackSmog
    {
        get
        {
            return blackSmog;
        }

        set
        {
            blackSmog = value;
        }
    }

    public GameObject SelectBubble
    {
        get
        {
            return selectBubble;
        }

        set
        {
            selectBubble = value;
        }
    }

    // left mouse button
    void OnMouseDown()
    {
        //discoverNeighbors();

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
                    Debug.Log("char is already moving: " + movingChar.IsCurrentlyMoving + "; this is: " + this.name + " selected is: "+selectedRoom);
                    if (!movingChar.IsCurrentlyMoving)
                    {
                        selectedRoom.pendingMovementsNorth.Add(new Movement(selectedRoom, this, movingChar));
                        movingChar.IsCurrentlyMoving = true;
                    }
                }
            }
            else if (selectedRoom.EastRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    Debug.Log("char is already moving: " + movingChar.IsCurrentlyMoving + "; this is: " + this.name + " selected is: " + selectedRoom);
                    if (!movingChar.IsCurrentlyMoving)
                    {
                        selectedRoom.pendingMovementsEast.Add(new Movement(selectedRoom, this, movingChar));
                        movingChar.IsCurrentlyMoving = true;
                    }
                }
            }
            else if (selectedRoom.WestRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    Debug.Log("char is already moving: " + movingChar.IsCurrentlyMoving + "; this is: " + this.name + " selected is: " + selectedRoom);
                    if (!movingChar.IsCurrentlyMoving)
                    {
                        selectedRoom.pendingMovementsSouth.Add(new Movement(selectedRoom, this, movingChar));
                        movingChar.IsCurrentlyMoving = true;
                    }
                }
            }
            else if (selectedRoom.SouthRoom == this)
            {
                foreach (Character movingChar in selectedRoom.SelectedCharacters)
                {
                    Debug.Log("char is already moving: " + movingChar.IsCurrentlyMoving + "; this is: " + this.name + " selected is: " + selectedRoom);
                    if (!movingChar.IsCurrentlyMoving)
                    {
                        selectedRoom.pendingMovementsWest.Add(new Movement(selectedRoom, this, movingChar));
                        movingChar.IsCurrentlyMoving = true;
                    }
                }
            }
        }
    }

    public void resolvePendingMovements()
    {
        Debug.Log(this.name+": " + pendingMovementsEast.Count + ", " + pendingMovementsNorth.Count + ", " + pendingMovementsSouth.Count + ", " + pendingMovementsWest.Count);
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
            Debug.Log(pendingMovement.Character + " tries to move from " + pendingMovement.Source + " to " + pendingMovement.Destination);
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

    public void sacrifice(int currentDayNum, Journal journal)
    {
        Debug.Log("Count is: " + characters.Count);
        if(selectedCharacters.Count > 0 && characters.Count >= 2)
        {
            Debug.Log("sacrifice!");
            Character victim = selectedCharacters[selectedCharacters.Count - 1];
            string storyText = victim.CharName + " gave his live for the greater Good!";
            selectedCharacters.Remove(victim);
            characters.Remove(victim);
            gameController.killCharacter(victim);
            int strengthBonus = victim.Strength / 4;
            int agilityBonus = victim.Agility / 4;
            int visionBonus = victim.Agility / 4;
            storyText += "\n";
            foreach (Character c in characters)
            {
                c.feast(strengthBonus, agilityBonus, visionBonus);
                storyText += c.CharName;
                if(characters.IndexOf(c) != characters.Count - 1)
                {
                    storyText += ", ";
                }
            }
            storyText += " gained " + visionBonus + " Vision, " + strengthBonus + " Strength and " + agilityBonus + " Agility from feasting on his flesh";
            journal.addStory(new Story(currentDayNum, storyText));
        }
        
    }

    public void discoverNeighbors()
    {
        gameObject.SetActive(true);

        GameObject temp;
        BlackSmog.SetActive(false);
        foreach (GameObject i in fades)
        {
            Destroy(i);
        }
        fades.Clear();

        if (NorthRoom != null)
        {
            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 1.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 2.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-.25f, 2.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (northRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(-.25f, 3f, 0);
                temp.transform.Rotate(0, 0, 180);
                NorthRoom.Fades.Add(temp);
            }

            NorthRoom.gameObject.SetActive(true);
        }
        if (SouthRoom != null)
        {
            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -1.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -2.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelVer, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(.25f, -2.75f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (southRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(.25f, -3f, 0);
                temp.transform.Rotate(0, 0, 0);
                SouthRoom.Fades.Add(temp);
            }

            SouthRoom.gameObject.SetActive(true);
        }
        if (WestRoom != null)
        {
            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.25f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-1.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-2.25f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(-2.75f, -.25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (WestRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(-3f, -.25f, 0);
                temp.transform.Rotate(0, 0, 270);
                WestRoom.Fades.Add(temp);
            }

            WestRoom.gameObject.SetActive(true);
        }
        if (EastRoom != null)
        {
            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.25f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(1.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(2.25f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            temp = Instantiate(TunnelHor, transform.position, Quaternion.identity) as GameObject;
            temp.transform.parent = transform;
            temp.transform.Translate(2.75f, .25f, 0);
            temp.GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;

            if (EastRoom.BlackSmog.activeSelf)
            {
                temp = Instantiate(Fade, transform.position, Quaternion.identity) as GameObject;
                temp.transform.Translate(3f, .25f, 0);
                temp.transform.Rotate(0, 0, 90);
                EastRoom.Fades.Add(temp);
            }

            EastRoom.gameObject.SetActive(true);
        }
    }

}