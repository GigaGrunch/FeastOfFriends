using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {

    int currentDayNum;
    Room selectedRoom;
    List<Room> activeRooms = new List<Room>();
    List<Movement> pendingMovements = new List<Movement>();
    Journal journal;

    void Start()
    {
        // initialize start characters here
        currentDayNum = 1;
        // TODO: replace with Constructor if Journal is no gameobject
        journal = FindObjectOfType<Journal>();
    }

    public Room SelectedRoom
    {
        get
        {
            return selectedRoom;
        }

        set
        {
            selectedRoom = value;
        }
    }

    public void onCharacterClicked(Character clickedCharacter)
    {
        if(SelectedRoom.SelectedCharacters.Contains(clickedCharacter))
        {
            SelectedRoom.SelectedCharacters.Remove(clickedCharacter);
        }
        else
        {
            SelectedRoom.SelectedCharacters.Add(clickedCharacter);
        }
    }

    public void onRoomClicked(Room clickedRoom)
    {
        SelectedRoom = clickedRoom;
    }

    void endTurn()
    {
        // do some stuff on turn end
        resolvePendingMovements();
        removeEmptyRooms();
        currentDayNum++;
    }

    private void resolvePendingMovements()
    {
        foreach(Movement pendingMovement in pendingMovements)
        {
            // test requirements here
            bool success = true;
            if(success)
            {
                Character character = pendingMovement.Character;
                journal.addStory(new Story(currentDayNum, character + " managed to enter an exciting new Room"));
                pendingMovement.Source.Characters.Remove(character);
                pendingMovement.Destination.Characters.Add(character);
                if (!activeRooms.Contains(pendingMovement.Destination))
                {
                    activeRooms.Add(pendingMovement.Destination);
                }
            }
        }
    }

    void removeEmptyRooms()
    {
        foreach (Room i in activeRooms)
        {
            if (i.Characters.Count <= 0)
            {
                activeRooms.Remove(i);
            }
        }
    }

    public void addPendingMovement(Movement pendingMovement)
    {
        pendingMovements.Add(pendingMovement);
    }
}
