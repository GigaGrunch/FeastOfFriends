using UnityEngine;
using System.Collections.Generic;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField]
    Sprite face;

    int currentDayNum;
    int nextDeathIn;
    Room selectedRoom;
    List<Room> activeRooms = new List<Room>();
    Journal journal;
    List<Character> characters;

    void Start()
    {
        // initialize start characters here
        currentDayNum = 1;
        nextDeathIn = 5;
        // TODO: replace with Constructor if Journal is no gameobject
        journal = FindObjectOfType<Journal>();

        selectedRoom = new Room();
        Character testCharacter1 = new Character();
        testCharacter1.Portrait = face;

        selectedRoom.Characters.Add(testCharacter1);

        GameObject.Find("Main Camera").GetComponent<InterfaceController>().SetRoomMembers(selectedRoom.Characters);
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
        if (SelectedRoom.SelectedCharacters.Contains(clickedCharacter))
        {
            SelectedRoom.SelectedCharacters.Remove(clickedCharacter);
        }
        else
        {
            SelectedRoom.SelectedCharacters.Add(clickedCharacter);
        }
    }

    public void onRoomSelected(Room clickedRoom)
    {
        SelectedRoom = clickedRoom;
    }

    void endTurn()
    {
        // do some stuff on turn end
        foreach (Room r in activeRooms)
        {
            r.resolvePendingMovements();
        }
        removeEmptyRooms();
        currentDayNum++;
        nextDeathIn--;
        if (nextDeathIn <= 0)
        {
            killRandomCharacter();
            nextDeathIn = 5;
        }
    }

    private void killRandomCharacter()
    {
        // kill random character without boni for the others
        int index = UnityEngine.Random.Range(0, characters.Count);
        Character victim = characters[index];
        foreach (Room i in activeRooms)
        {
            i.SelectedCharacters.Remove(victim);
            i.Characters.Remove(victim);
        }
        killCharacter(victim);
    }

    public void killCharacter(Character victim)
    {
        characters.Remove(victim);
        Destroy(victim.gameObject);
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
        source.Characters.Remove(character);
        destination.Characters.Add(character);

        journal.addStory(new Story(currentDayNum, character + " managed to enter an exciting new Room"));
        activeRooms.Remove(source);
        if (!activeRooms.Contains(destination))
        {
            activeRooms.Add(destination);
        }
    }
}
