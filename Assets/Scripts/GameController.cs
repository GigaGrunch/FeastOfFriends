using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour {

    int currentDayNum;
    int nextDeathIn;
    Room selectedRoom;
    List<Room> activeRooms = new List<Room>();
    Journal journal;

    void Start()
    {
        // initialize start characters here
        currentDayNum = 1;
        nextDeathIn = 5;
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
        foreach(Room r in activeRooms)
        {
            r.resolvePendingMovements();
        }
        removeEmptyRooms();
        currentDayNum++;
        nextDeathIn--;
        if(nextDeathIn <= 0)
        {
            killRandomCharacter();
            nextDeathIn = 5;
        }
    }

    private void killRandomCharacter()
    {
        // kill random character without boni for the others
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

    public void OnPlayerMovementSuccess(Room source, Room destination, Character character)
    {
        journal.addStory(new Story(currentDayNum, character + " managed to enter an exciting new Room"));
        activeRooms.Remove(source);
        if (!activeRooms.Contains(destination))
        {
            activeRooms.Add(destination);
        }
    }
}
